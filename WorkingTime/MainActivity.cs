using Android.App;
using Android.Widget;
using Android.OS;
using RadiusNetworks.IBeaconAndroid;
using System.Linq;
using Android.Content;
using Android.Support.V4.App;

namespace WorkingTime
{
    [Activity(Label = "WorkingTime", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, IBeaconConsumer
    {
		private DB dB;
		private const string UUID = "B5B182C7-EAB1-4988-AA99-B5C1517008D9";
        private const string monkeyId = "abeacon_583B";

		bool _paused;
        IBeaconManager _iBeaconManager;
        MonitorNotifier _monitorNotifier;
        RangeNotifier _rangeNotifier;
        Region _monitoringRegion;
        Region _rangingRegion;
		int _previousProximity;

        protected override void OnStart()
        {
            base.OnStart();

        }


        public MainActivity()
		{
			_iBeaconManager = IBeaconManager.GetInstanceForApplication(this);

            _monitorNotifier = new MonitorNotifier();
            _rangeNotifier = new RangeNotifier();

            _monitoringRegion = new Region(monkeyId, UUID, null, null);
            _rangingRegion = new Region(monkeyId, UUID, null, null);
		}

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
			SetContentView(WorkingTime.Resource.Layout.Main1);

            dB = new DB();

            var listWorkTime = dB.GetWorkTime();

            ListView listView = FindViewById<ListView>(Resource.Id.listWorkTime);

			TimeWorkAdapter arrayAdapter = null;
         
			arrayAdapter = new TimeWorkAdapter(this, listWorkTime, (TimeWork t)=>{
				dB.DeleteWorkItem(t);
				listWorkTime.Remove(t);
				if (arrayAdapter != null)
					arrayAdapter.NotifyDataSetChanged();
			});

            listView.Adapter = arrayAdapter;


            TextView txtTimeNow = FindViewById<TextView>(Resource.Id.txtDateTime),
            txtExitTime = FindViewById<TextView>(Resource.Id.txtExitTime);
            Button translateButton = FindViewById<Button>(Resource.Id.btnCalculateExit);


            async void Heartbeat()
            {
                while (true)
                {
                    await System.Threading.Tasks.Task.Delay(1000);
                    txtTimeNow.Text = System.DateTime.Now.ToString("HH:mm:ss");
                }
            }

            Heartbeat();

            translateButton.Click += (object sender, System.EventArgs e) => {

                System.DateTime endWorkTime = System.DateTime.Now.AddHours(8).AddMinutes(33);
                txtExitTime.Text = endWorkTime.ToString("HH:mm:ss");

                var timeWork = dB.InsertWorkTime(System.DateTime.Now, endWorkTime);

				listWorkTime.Insert(0, timeWork);
				arrayAdapter.NotifyDataSetChanged();
            };


            //Ibeacon
			_iBeaconManager.Bind(this);

            _monitorNotifier.EnterRegionComplete += EnteredRegion;
            _monitorNotifier.ExitRegionComplete += ExitedRegion;

            _rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;


        }

		protected override void OnResume()
        {
            base.OnResume();
            _paused = false;
        }

        protected override void OnPause()
        {
            base.OnPause();
            _paused = true;
        }

		void EnteredRegion(object sender, MonitorEventArgs e)
        {
            if (_paused)
            {
                ShowNotification();
                //TODO complete
            }
        }

        void ExitedRegion(object sender, MonitorEventArgs e)
        {
        }

		private void ShowNotification(string distance = null)
        {
            var resultIntent = new Intent(this, typeof(MainActivity));
            resultIntent.AddFlags(ActivityFlags.ReorderToFront);
            var pendingIntent = PendingIntent.GetActivity(this, 0, resultIntent, PendingIntentFlags.UpdateCurrent);
			var notificationId = Resource.String.text_notification;

            var builder = new NotificationCompat.Builder(this)
			    .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle("Working Time")
			    .SetContentText((distance == null ? GetText(notificationId) : $"Distanza -> {distance}"))
                .SetContentIntent(pendingIntent)
                .SetAutoCancel(true);

            var notification = builder.Build();

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
			notificationManager.Notify(notificationId, notification);
        }

		void RangingBeaconsInRegion(object sender, RangeEventArgs e)
        {
            if (e.Beacons.Count > 0)
            {
                var beacon = e.Beacons.FirstOrDefault();
                var message = string.Empty;

                switch ((ProximityType)beacon.Proximity)
                {
                    case ProximityType.Immediate:
						ShowNotification("Vicinissimo");
                        //UpdateDisplay("You found the monkey!", Color.Green);
                        break;
                    case ProximityType.Near:
						ShowNotification("Vicino");
                        //UpdateDisplay("You're getting warmer", Color.Yellow);
                        break;
                    case ProximityType.Far:
						ShowNotification("Lontano");
                        //UpdateDisplay("You're freezing cold", Color.Blue);
                        break;
                    case ProximityType.Unknown:
                        //UpdateDisplay("I'm not sure how close you are to the monkey", Color.Red);
                        break;
                }

                _previousProximity = beacon.Proximity;
            }
        }

		public void OnIBeaconServiceConnect()
		{
			_iBeaconManager.SetMonitorNotifier(_monitorNotifier);
            _iBeaconManager.SetRangeNotifier(_rangeNotifier);

            _iBeaconManager.StartMonitoringBeaconsInRegion(_monitoringRegion);
            _iBeaconManager.StartRangingBeaconsInRegion(_rangingRegion);
		}

		protected override void OnDestroy()
        {
            base.OnDestroy();

            _monitorNotifier.EnterRegionComplete -= EnteredRegion;
            _monitorNotifier.ExitRegionComplete -= ExitedRegion;

            _rangeNotifier.DidRangeBeaconsInRegionComplete -= RangingBeaconsInRegion;

            _iBeaconManager.StopMonitoringBeaconsInRegion(_monitoringRegion);
            _iBeaconManager.StopRangingBeaconsInRegion(_rangingRegion);
            _iBeaconManager.UnBind(this);
        }
	}
}


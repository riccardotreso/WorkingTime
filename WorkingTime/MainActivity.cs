using Android.App;
using Android.Widget;
using Android.OS;

namespace WorkingTime
{
    [Activity(Label = "WorkingTime", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
		private DB dB;
        protected override void OnStart()
        {
            base.OnStart();

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



        }
    }
}


package md582d09bd90cbd4b477b852e49c3e6206c;


public class MainActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.radiusnetworks.ibeacon.IBeaconConsumer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onStart:()V:GetOnStartHandler\n" +
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"n_getApplicationContext:()Landroid/content/Context;:GetGetApplicationContextHandler:RadiusNetworks.IBeaconAndroid.IBeaconConsumerInvoker, Android-iBeacon-Service\n" +
			"n_bindService:(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z:GetBindService_Landroid_content_Intent_Landroid_content_ServiceConnection_IHandler:RadiusNetworks.IBeaconAndroid.IBeaconConsumerInvoker, Android-iBeacon-Service\n" +
			"n_onIBeaconServiceConnect:()V:GetOnIBeaconServiceConnectHandler:RadiusNetworks.IBeaconAndroid.IBeaconConsumerInvoker, Android-iBeacon-Service\n" +
			"n_unbindService:(Landroid/content/ServiceConnection;)V:GetUnbindService_Landroid_content_ServiceConnection_Handler:RadiusNetworks.IBeaconAndroid.IBeaconConsumerInvoker, Android-iBeacon-Service\n" +
			"";
		mono.android.Runtime.register ("WorkingTime.MainActivity, WorkingTime", MainActivity.class, __md_methods);
	}


	public MainActivity ()
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("WorkingTime.MainActivity, WorkingTime", "", this, new java.lang.Object[] {  });
	}


	public void onStart ()
	{
		n_onStart ();
	}

	private native void n_onStart ();


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public void onDestroy ()
	{
		n_onDestroy ();
	}

	private native void n_onDestroy ();


	public android.content.Context getApplicationContext ()
	{
		return n_getApplicationContext ();
	}

	private native android.content.Context n_getApplicationContext ();


	public boolean bindService (android.content.Intent p0, android.content.ServiceConnection p1, int p2)
	{
		return n_bindService (p0, p1, p2);
	}

	private native boolean n_bindService (android.content.Intent p0, android.content.ServiceConnection p1, int p2);


	public void onIBeaconServiceConnect ()
	{
		n_onIBeaconServiceConnect ();
	}

	private native void n_onIBeaconServiceConnect ();


	public void unbindService (android.content.ServiceConnection p0)
	{
		n_unbindService (p0);
	}

	private native void n_unbindService (android.content.ServiceConnection p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AnylineXamarinApp.Energy;

namespace AnylineXamarinApp
{
    [Activity(Label = "Anyline Xamarin Examples", 
        MainLauncher = true, Icon = "@drawable/ic_launcher", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, 
        HardwareAccelerated = true)]
    public class MainActivity : Activity
    {
        //INSERT YOUR LICENSE KEY HERE
        public const string LicenseKey = "eyAiYW5kcm9pZElkZW50aWZpZXIiOiBbICJBVC5BbnlsaW5lLlhhbWFyaW4uQXBwLkRyb2lkIiwgIkFULkFueWxpbmUuWGFtYXJpbi5Gb3Jtcy5BcHAuRHJvaWQiIF0sICJkZWJ1Z1JlcG9ydGluZyI6ICJvbiIsICJpb3NJZGVudGlmaWVyIjogWyAiQVQuQW55bGluZS5YYW1hcmluLkFwcC5pT1MiLCAiQVQuQW55bGluZS5YYW1hcmluLkZvcm1zLkFwcC5pT1MiIF0sICJsaWNlbnNlS2V5VmVyc2lvbiI6IDIsICJtYWpvclZlcnNpb24iOiAiMyIsICJwaW5nUmVwb3J0aW5nIjogdHJ1ZSwgInBsYXRmb3JtIjogWyAiaU9TIiwgIkFuZHJvaWQiIF0sICJzY29wZSI6IFsgIkFMTCIgXSwgInNob3dXYXRlcm1hcmsiOiB0cnVlLCAidG9sZXJhbmNlRGF5cyI6IDkwLCAidmFsaWQiOiAiMjAyMC0wMS0wMSIgfQprcS9WL0wrSGlpN0NzL2tXa1E5VWRzbGxzd0hOanphelZEZ2Z2WU1LLytJN1VHYmlITy9SblMrdGZIeUZxQmlJCkN3QXkrdkk5RnJpOVc5MStGdjJTS2FJNS8vLzZhUVgyVXlSVC9CaVRKM1QzTXBVOEIrMWpFZTQxbCtXejRqaFgKMlZ6dENpT2E3cit3d2RlTm1GUFpxdGVUTG5BRmgxQWgycDZpMzgyMWhOb3FsVHNxcFlJdjN3cWdCbWg5clh2WgpBM01pRnpkZ0dab1gzbzNINzFGRUtJME9JSy9ZRkNJRk5nVEI0MFhBM3ZTOXk2ak1FR2E5bjVQRHY5MU5NZEFRCnlHTzcxRVVuZE9ndmJmTkJWbVJYNUR1MGVrZ0RGYUNFMUwweVpUQ3dhMFJVTStLSE9PcXA3TThYOWVFdjJ0RVkKVEcyejdydGQ5YytiRlBvTU5vcUpwZz09Cg==";
        
        /// <summary>
        /// Called when the activity is starting.
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle)
        {

            Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainActivity);

            ListView listView = FindViewById<ListView>(Resource.Id.listView);

            ActivityListAdapter listAdapter = new ActivityListAdapter(this);
            listView.Adapter = listAdapter;

            //adapt height of listView so it fits.
            Util.SetListViewHeightBasedOnChildren(listView);

            listView.ItemClick += (s,a) =>
            {
                if (listAdapter.GetItemViewType(a.Position) == ActivityListAdapter.TypeHeader)
                    return;
                
                //starts activity from the given class name in example_activities.xml
                var type = Type.GetType(listAdapter.ClassName(a.Position));

                if (type == null)
                    return;
                try
                {
                    var intent = new Intent(ApplicationContext, type);

                    // we generate the energy activity with different radiobuttons, depending on the use-case
                    // therefore we add which scan modes should be selectable
                    if (type == typeof(EnergyActivity))
                        intent.PutExtra("OBJECT", listAdapter.GetItem(a.Position).ToString());                        
                    
                    StartActivity(intent);
                }
                catch(Java.Lang.ClassNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

            };
        }
                
        /// <summary>
        /// Kills the App when Back is pressed
        /// </summary>
        public override void OnBackPressed()
        {
            Finish();
            Process.KillProcess(Process.MyPid());
        }
    }
}


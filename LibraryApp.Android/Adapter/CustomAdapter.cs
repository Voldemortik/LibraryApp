using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using Android.Runtime;
using Android.App;
using Android.Content.PM;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

using LibraryApp.Models;
using LibraryApp.Data;


namespace LibraryApp.Droid.Adapter
{
    public class CustomAdapter : BaseAdapter
    {
        private MainActivity mainActivity;
        private List<Author> AuthorNames;
        private  DataBase dbHelper;

        public CustomAdapter(MainActivity mainActivity, List<Author> AuthorNamesList, DataBase dbHelper)
        {
            this.mainActivity = mainActivity;
            this.AuthorNames = AuthorNamesList;
            this.dbHelper = dbHelper;
        }
       
        public override int Count { get { return AuthorNames.Count; } }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override  View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mainActivity.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.Row, null);

            Button AutName = view.FindViewById<Button>(Resource.Id.Author_name);
            Button btnDelete = view.FindViewById<Button>(Resource.Id.btnDelete);

            AutName.Text = AuthorNames[position].AuthorName;
            AutName.Click += mainActivity.AutName_Click;

            btnDelete.Click += async delegate
            {
                var delaut = AuthorNames[position];
                await dbHelper.DeleteDataAsync(delaut);
                mainActivity.LoadAuthorList(); 
            };
            return view;
        }

        
    }
}
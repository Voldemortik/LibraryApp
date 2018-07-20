using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using Android.Runtime;
using Android.OS;
using Android.App;
using Android.Content.PM;
using Android.Support.V7.App;

using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

using LibraryApp.Models;
using LibraryApp.Data;

using Newtonsoft.Json;

namespace LibraryApp.Droid.Adapter
{
    class BookAdapter:BaseAdapter
    {
        private BookListActivity bookActivity;
        private List<Book> BookNames;
        private BookDataBase BookdbHelper;
        public BookAdapter(BookListActivity BookActivity, List<Book> BookNamesList, BookDataBase dbHelper)
        {
            this.bookActivity = BookActivity;

            this.BookNames = BookNamesList;
            this.BookdbHelper = dbHelper;
        }
        public override int Count { get { return BookNames.Count; } }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override long GetItemId(int position)
        {
            
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)bookActivity.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.BookRow, null);
            
            Button BookName = view.FindViewById<Button>(Resource.Id.Book_name);
            Button btnDelete = view.FindViewById<Button>(Resource.Id.btnBookDelete);

            BookName.Text = BookNames[position].BookName;

            BookName.Click += delegate
              {
                  Intent intent = new Intent(bookActivity, typeof(BookTitlePageActivity));
                  intent.PutExtra("titlepage_book", Newtonsoft.Json.JsonConvert.SerializeObject(BookNames[position]));

                  bookActivity.ActivityStart(intent);
              };
            btnDelete.Click += async delegate
              {
                var delaut = BookNames[position];
                await BookdbHelper.DeleteDataAsync(delaut);
                bookActivity.LoadBookList(); 
              };
            return view;
        }
    }
}
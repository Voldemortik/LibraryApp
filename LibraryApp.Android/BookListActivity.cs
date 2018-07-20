using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using LibraryApp.Data;
using LibraryApp.Models;

using Newtonsoft.Json;

namespace LibraryApp.Droid
{
    [Activity(Label = "BookList", Theme = "@style/Theme.AppCompat.Light")]
    public class BookListActivity : MainActivity
    {
        public ObservableCollection<Book> BookNames { get; set; }
        DataBase AuthorListInBook=new DataBase();
        List<Book> book = new List<Book>();
        Adapter.BookAdapter Adapter;
        Author CurrentAutToActivity;
        Random x = new Random();
        BookDataBase BookData;
        EditText edtBookName;
        Author CurrentAut;
        ListView lstBook;
        
        public override   bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            { 
              
                case Resource.Id.action_add:
                    edtBookName = new EditText(this);
                    
                    Android.Support.V7.App.AlertDialog alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle("Add New Book")
                        .SetView(edtBookName)
                        .SetPositiveButton("Add", OkAction)
                        .SetNegativeButton("Cancel", CancelAction)
                        .Create();
                    alertDialog.Show();
                    return true;
                case Resource.Id.action_cancel:

                      string autlst = Intent.GetStringExtra("AuthorListdata") ?? "data not available";
                       AuthorListInBook = JsonConvert.DeserializeObject<DataBase>(autlst);

                    Intent intent = new Intent(this, typeof(MainActivity));

                    CurrentAut.AuthorBook = book;
                    CurrentAutToActivity = CurrentAut;

                    UpdateList(CurrentAutToActivity,AuthorListInBook);

                    intent.PutExtra("AuthorListdata", JsonConvert.SerializeObject(AuthorListInBook));

                        StartActivity(intent);
                    Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            
        }
        private async void OkAction(object sender, DialogClickEventArgs e)
        {
            if (edtBookName.Text == "")
            {
                Toast.MakeText(this, "Ошибка:Введите название книги", ToastLength.Short).Show();

            }
            else if (!(TestBookName(book, edtBookName.Text)))
            {
                Toast.MakeText(this, "Ошибка:Такая книга  уже есть в списке", ToastLength.Short).Show();

            }
            else
            {
                var _book = new Book()
                {
                    BookName = edtBookName.Text,
                    BookId = Guid.NewGuid().ToString(),
                    BookPage = x.Next(1, 1000),
                };
                await BookData.AddDataAsync(_book);

                LoadBookList();
            }
        }
       
        
        public async void LoadBookList()
        {
            book.Clear();

                var bookList = await BookData.GetDataAsync(true);
                foreach (var name in bookList)
                {
                    book.Add(name);
                 
                }
          
               Adapter = new Adapter.BookAdapter(this, book, BookData);
                lstBook.Adapter = Adapter;
        }
        public async void UpdateList(Author author,DataBase AutList)
        {
            await AutList.UpdateDataAsync(author);

        }
        public async void AddList()
        {
            List<Book> BookNames = CurrentAut.AuthorBook;

            if (BookNames.Count != 0)
            {
                foreach (var name in BookNames)
                {
                    await BookData.AddDataAsync(name);
                }


            }

        }
        public bool TestBookName(List<Book> CurrentListBook, string Book_Name)
        {
            var Test = CurrentListBook.Where((Book TestArg) => TestArg.BookName == Book_Name).FirstOrDefault();
            if (Test == null)
            {
                return true;

            }
            return false;
        }
        public void ActivityStart(Intent intent)
        {
            intent.PutExtra("data", JsonConvert.SerializeObject(CurrentAut));
                StartActivity(intent);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
             SetContentView(Resource.Layout.ListBookActivity);

            string Aut = Intent.GetStringExtra("data") ?? "data not available";
             CurrentAut = JsonConvert.DeserializeObject<Author>(Aut);
            book = new List<Book>();
             BookData = new BookDataBase();
              lstBook = FindViewById<ListView>(Resource.Id.lstBooks);
               AddList();
            LoadBookList();

        }
    }
}
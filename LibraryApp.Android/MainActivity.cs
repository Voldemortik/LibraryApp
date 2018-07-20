using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using System;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using LibraryApp.Data;
using LibraryApp.Models;

using Newtonsoft.Json;
namespace LibraryApp.Droid
{
    [Activity(Label = "LibraryApp", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        public ObservableCollection<Author> Names { get; set; }
        public  List<Author> author = new List<Author>();
        Adapter.CustomAdapter Adapter;
        public DataBase AuthorData;
        EditText edtAuthor;
        string AutListBook;
        ListView lstAut;
        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Add_Menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_add:
                    edtAuthor = new EditText(this);
                    Android.Support.V7.App.AlertDialog alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle("Add New Author")
                        .SetView(edtAuthor)
                        .SetPositiveButton("Add", OkAction)
                        .SetNegativeButton("Cancel", CancelAction)
                        .Create();
                    alertDialog.Show();
                    return true;
                case Resource.Id.action_cancel:

                    Intent startMain = new Intent(Intent.ActionMain);
                        startMain.AddCategory(Intent.CategoryHome);
                        startMain.SetFlags(ActivityFlags.NewTask);
                        StartActivity(startMain);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        private void CancelAction(object sender, DialogClickEventArgs e)
        {

        }
        private async void OkAction(object sender, DialogClickEventArgs e)
        {


            if (edtAuthor.Text=="")
            {
                Toast.MakeText(this, "Ошибка:Введите имя", ToastLength.Short).Show();

            }
            else if (!(TestAuthor(author,edtAuthor.Text)))
            {
                Toast.MakeText(this, "Ошибка:Такой автор уже есть в списке", ToastLength.Short).Show();

            }
            else
            {
                var author = new Author()
                {
                    AuthorName = edtAuthor.Text,
                    AuthorId = Guid.NewGuid().ToString(),
                    AuthorBook = new List<Book>(),
                };
                await AuthorData.AddDataAsync(author);

                LoadAuthorList();
            }
        }
        public bool TestAuthor(List<Author>CurrentListAut,string author_Name)
        {
            var Test = CurrentListAut.Where((Author TestArg) => TestArg.AuthorName == author_Name).FirstOrDefault();
            if (Test == null)
            {
                return true;
               
            }
            return false;
        }
        public async void LoadAuthorList()
        {
            author.Clear();

            var AutList = await AuthorData.GetDataAsync(true);
            foreach(var name in AutList)
            {
                author.Add(name);
                
            }
            Adapter = new Adapter.CustomAdapter(this, author, AuthorData);
            lstAut.Adapter = Adapter;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
          

                AutListBook = Intent.GetStringExtra("AuthorListdata") ?? "data not available";

                if (AutListBook != "data not available")
                    AuthorData = JsonConvert.DeserializeObject<DataBase>(AutListBook);
                else
                    AuthorData = new DataBase();
            
            
            lstAut = FindViewById<ListView>(Resource.Id.lstAuthor);

            LoadAuthorList();

        }

        public void AutName_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Author Taut = author.Where((Author arg) => arg.AuthorName == btn.Text).FirstOrDefault();

            Intent intent = new Intent(this, typeof(BookListActivity));


            intent.PutExtra("data", Newtonsoft.Json.JsonConvert.SerializeObject(Taut));
            intent.PutExtra("AuthorListdata", Newtonsoft.Json.JsonConvert.SerializeObject(AuthorData));
                StartActivity(intent);

        }
    }
}


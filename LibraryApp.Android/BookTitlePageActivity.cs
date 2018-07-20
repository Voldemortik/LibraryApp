using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using LibraryApp.Models;
namespace LibraryApp.Droid
{
    [Activity(Label ="")]
    public class BookTitlePageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.TitlePage);

            string Aut = Intent.GetStringExtra("data") ?? "data not available";
                Author CurAut = JsonConvert.DeserializeObject<Author>(Aut);

            string book = Intent.GetStringExtra("titlepage_book") ?? "data not available";
                Book CurBook = JsonConvert.DeserializeObject<Book>(book);

            TextView AutName = FindViewById<TextView>(Resource.Id.titlepage_AuthorName);
                TextView BookName = FindViewById<TextView>(Resource.Id.titlepage_BookName);
                    TextView Pages = FindViewById<TextView>(Resource.Id.titlepage_Pages);

            AutName.Text = CurAut.AuthorName;
                BookName.Text = CurBook.BookName;
                    Pages.Text = CurBook.BookPage.ToString();
    
            
            
        }
        
      
    }
}
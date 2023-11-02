using LMSData;
using LMSData.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;

        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }
        public void Add(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);
        }

        public LibraryAsset GetByID(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return 
                GetAll()
                .FirstOrDefault(asset => asset.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetByID(id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            //Discriminator
            if (_context.Books.Any(book => book.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(book => book.Id == id).DeweyIndex;
            }
            else return "";
        }

        public string GetIsbn(int id)
        {
            if (_context.Books.Any(a => a.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(a => a.Id == id).ISBN;
            }
            else return "";
        }

        public string GetTitle(int id)
        {
            if (_context.Books.Any(a => a.Id == id))
            {
                return _context.Books
                    .FirstOrDefault(a => a.Id == id).Title;
            }
            else return "";
        }

        public string GetType(int id)
        {
            var book = _context.LibraryAssets.OfType<Book>()
                .Where(B => B.Id == id);
            return book.Any() ? "Book" : "Video"; 
        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = _context.LibraryAssets.OfType<Book>()
                .Where(asset => asset.Id == id).Any();

            var isVideo = _context.LibraryAssets.OfType<Video>()
                .Where(asset => asset.Id == id).Any();
            return isBook ?
                _context.Books.FirstOrDefault(book => book.Id == id).Author :
                _context.Videos.FirstOrDefault(Video => Video.Id == id).Director
                ?? "Unknown";

        }
    }
}
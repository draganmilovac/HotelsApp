using HotelsApp.Data.Models;
using HotelsApp.Infrastructure.Models;

namespace HotelsApp.Tests.Lists
{
    public class HotelLists
    {
        #region Constructors
        public HotelLists()
        {
            Hotels = new Hotels();
            PopulateList();
        }
        #endregion

        #region Properties

        public Hotels Hotels { get; set; }
        #endregion

        #region Private methods

        private void PopulateList()
        {
            Hotels.Add(new Hotel()
            {
                Id = 1,
                Name = "Hotel 1",
                Price = 20,
                Latitude = 45.254374284753254,
                Longitude = 19.821862950036017
            });
            Hotels.Add(new Hotel()
            {
                Id = 2,
                Name = "Hotel 2",
                Price = 20,
                Latitude = 45.2211327840916,
                Longitude = 19.848985445749207
            });
            Hotels.Add(new Hotel()
            {
                Id = 3,
                Name = "Hotel 3",
                Price = 20,
                Latitude = 45.237212010810886,
                Longitude = 19.810189977197428
            });
            Hotels.Add(new Hotel()
            {
                Id = 4,
                Name = "Hotel 4",
                Price = 20,
                Latitude = 45.25689124664803,
                Longitude = 19.814676541895956
            });
        }
        #endregion
    }
}

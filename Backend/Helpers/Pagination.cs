using Backend.Dtos;

namespace Backend.Helpers {
    public class Pagination <T> where T : class{
        public Pagination(int pageIndex, int pageSize, int totalItems, IReadOnlyList<ProductToReturnDto> data) {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
            Data1 = data;
        }

        public int PageIndex { get; set; }
   
     public int PageSize { get; set; }
   
      public int Count { get; set; }
     
       public IReadOnlyList<T> Data { get; set; }
        public int TotalItems { get; }
        public IReadOnlyList<ProductToReturnDto> Data1 { get; }
    }
}

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ProductPriceListElement
    {
        public ProductPriceListElement(PRODUCT_PRICE productPrice)
        {
            ProductPrice = productPrice;
            Staff = productPrice.STAFF;
            Post = Staff.POST;
        }

        public PRODUCT_PRICE ProductPrice { get; set; }

        public STAFF Staff { get; set; }

        public POST Post { get; set; }
    }
}
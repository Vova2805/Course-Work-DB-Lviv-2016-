namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ProductListElement
    {
        public ProductListElement(PRODUCT_INFO ProductInfo)
        {
            this.ProductInfo = ProductInfo;
            isAdded = false;
        }

        public PRODUCT_INFO ProductInfo { get; set; }
        public bool isAdded { get; set; }
    }
}
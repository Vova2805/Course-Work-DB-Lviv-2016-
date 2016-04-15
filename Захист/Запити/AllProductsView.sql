CREATE VIEW RELEASED_PRODUCT_VIEW AS
SELECT        dbo.CATEGORY.CATEGORY_TITLE as Категорія, dbo.PRODUCT_INFO.PRODUCT_TITLE as Продукт,
 dbo.PRODUCT_INFO.QUANTITY_IN_PACKAGE as Кількість_в_пачці, dbo.PACKAGE_DESCRIPTION.WIDTH as Ширина,
  dbo.PACKAGE_DESCRIPTION.HEIGTH as Висота, 
                         dbo.PACKAGE_DESCRIPTION.LENGTH as Довжина, dbo.RELEASED_PRODUCT.PRODUCTION_DATE as Дата_виготовлення,
						  dbo.RELEASED_PRODUCT.EXPIRED_DATE as Закінчення_строку_придатності,
						   dbo.RELEASED_PRODUCT.QUANTITY as Кількість, 
                         dbo.PRODUCT_INFO.PRODUCT_PRICE as Ціна, dbo.WAREHOUSE.CAPACITY as Місткість,
						  dbo.WAREHOUSE.FREE_SPACE as Вільного_місця,
						   dbo.WAREHOUSE.PHONE_NUMBER as Телефон, dbo.ADDRESS.CITY as Місто , dbo.ADDRESS.STREET as Вулиця, 
                         dbo.ADDRESS.BUILDING_NUMBER as Будівля
FROM            dbo.PRODUCT_INFO INNER JOIN
                         dbo.CATEGORY ON dbo.PRODUCT_INFO.CATEGORY_ID = dbo.CATEGORY.CATEGORY_ID INNER JOIN
                         dbo.PACKAGE_DESCRIPTION ON dbo.PRODUCT_INFO.PACK_DESCR_ID = dbo.PACKAGE_DESCRIPTION.PACK_DESCR_ID INNER JOIN
                         dbo.RELEASED_PRODUCT ON dbo.PRODUCT_INFO.PRODUCT_INFO_ID = dbo.RELEASED_PRODUCT.PRODUCT_INFO_ID INNER JOIN
                         dbo.WAREHOUSE ON dbo.RELEASED_PRODUCT.WAREHOUSE_ID = dbo.WAREHOUSE.WAREHOUSE_ID INNER JOIN
                         dbo.ADDRESS ON dbo.WAREHOUSE.ADDRESS = dbo.ADDRESS.ADDRESS_ID
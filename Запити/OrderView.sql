CREATE VIEW ORDER_VIEW AS
SELECT        dbo.CLIENT.CLIENT_NAME as Імя, dbo.CLIENT.CLIENT_SURNAME as Прізвище,
 dbo.CLIENT.CLIENT_MIDDLE_NAME as По_батькові,
 dbo.CLIENT.MOBILE_PHONE as Мобільний, dbo.CLIENT.COMPANY_PHONE as Компанії, dbo.CLIENT.EMAIL as email, dbo.ADDRESS.CITY as Місто, 
                         dbo.SALE_ORDER.ORDER_DATE as Дата_замовлення,
						 dbo.SALE_ORDER.REQUIRED_DATE as Крайній_строк, dbo.CATEGORY.CATEGORY_TITLE as Категорія,
						  dbo.PRODUCT_INFO.PRODUCT_TITLE as Продукт, dbo.PRODUCT_INFO.QUANTITY_IN_PACKAGE as Кількість_в_упаковці, 
                          dbo.ORDER_PRODUCT.QUANTITY_IN_ORDER as Кількість_замовлено,
						  dbo.SALE_ORDER.ORDER_STATUS as Статус_замовлення, dbo.SALE_ORDER.DISCOUNT as Знижка,
						  dbo.SALE_ORDER.TOTAL as Загальна_сума
FROM            dbo.CATEGORY INNER JOIN
                         dbo.PRODUCT_INFO ON dbo.CATEGORY.CATEGORY_ID = dbo.PRODUCT_INFO.CATEGORY_ID INNER JOIN
                         dbo.ORDER_PRODUCT ON dbo.PRODUCT_INFO.PRODUCT_INFO_ID = dbo.ORDER_PRODUCT.PRODUCT_INFO_ID INNER JOIN
                         dbo.SALE_ORDER ON dbo.ORDER_PRODUCT.SALE_ORDER_ID = dbo.SALE_ORDER.SALE_ORDER_ID INNER JOIN
                         dbo.CLIENT ON dbo.SALE_ORDER.CLIENT_ID = dbo.CLIENT.CLIENT_ID INNER JOIN
                         dbo.ADDRESS ON dbo.CLIENT.ADDRESS = dbo.ADDRESS.ADDRESS_ID
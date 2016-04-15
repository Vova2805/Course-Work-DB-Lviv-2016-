CREATE VIEW ORDER_VIEW AS
SELECT        dbo.CLIENT.CLIENT_NAME as ���, dbo.CLIENT.CLIENT_SURNAME as �������,
 dbo.CLIENT.CLIENT_MIDDLE_NAME as ��_�������,
 dbo.CLIENT.MOBILE_PHONE as ��������, dbo.CLIENT.COMPANY_PHONE as ������, dbo.CLIENT.EMAIL as email, dbo.ADDRESS.CITY as ̳���, 
                         dbo.SALE_ORDER.ORDER_DATE as ����_����������,
						 dbo.SALE_ORDER.REQUIRED_DATE as ������_�����, dbo.CATEGORY.CATEGORY_TITLE as ��������,
						  dbo.PRODUCT_INFO.PRODUCT_TITLE as �������, dbo.PRODUCT_INFO.QUANTITY_IN_PACKAGE as ʳ������_�_��������, 
                          dbo.ORDER_PRODUCT.QUANTITY_IN_ORDER as ʳ������_���������,
						  dbo.SALE_ORDER.ORDER_STATUS as ������_����������, dbo.SALE_ORDER.DISCOUNT as ������,
						  dbo.SALE_ORDER.TOTAL as ��������_����
FROM            dbo.CATEGORY INNER JOIN
                         dbo.PRODUCT_INFO ON dbo.CATEGORY.CATEGORY_ID = dbo.PRODUCT_INFO.CATEGORY_ID INNER JOIN
                         dbo.ORDER_PRODUCT ON dbo.PRODUCT_INFO.PRODUCT_INFO_ID = dbo.ORDER_PRODUCT.PRODUCT_INFO_ID INNER JOIN
                         dbo.SALE_ORDER ON dbo.ORDER_PRODUCT.SALE_ORDER_ID = dbo.SALE_ORDER.SALE_ORDER_ID INNER JOIN
                         dbo.CLIENT ON dbo.SALE_ORDER.CLIENT_ID = dbo.CLIENT.CLIENT_ID INNER JOIN
                         dbo.ADDRESS ON dbo.CLIENT.ADDRESS = dbo.ADDRESS.ADDRESS_ID
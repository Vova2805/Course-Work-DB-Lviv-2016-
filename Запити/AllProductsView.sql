CREATE VIEW RELEASED_PRODUCT_VIEW AS
SELECT        dbo.CATEGORY.CATEGORY_TITLE as ��������, dbo.PRODUCT_INFO.PRODUCT_TITLE as �������,
 dbo.PRODUCT_INFO.QUANTITY_IN_PACKAGE as ʳ������_�_�����, dbo.PACKAGE_DESCRIPTION.WIDTH as ������,
  dbo.PACKAGE_DESCRIPTION.HEIGTH as ������, 
                         dbo.PACKAGE_DESCRIPTION.LENGTH as �������, dbo.RELEASED_PRODUCT.PRODUCTION_DATE as ����_������������,
						  dbo.RELEASED_PRODUCT.EXPIRED_DATE as ���������_������_����������,
						   dbo.RELEASED_PRODUCT.QUANTITY as ʳ������, 
                         dbo.PRODUCT_INFO.PRODUCT_PRICE as ֳ��, dbo.WAREHOUSE.CAPACITY as ̳������,
						  dbo.WAREHOUSE.FREE_SPACE as ³������_����,
						   dbo.WAREHOUSE.PHONE_NUMBER as �������, dbo.ADDRESS.CITY as ̳��� , dbo.ADDRESS.STREET as ������, 
                         dbo.ADDRESS.BUILDING_NUMBER as ������
FROM            dbo.PRODUCT_INFO INNER JOIN
                         dbo.CATEGORY ON dbo.PRODUCT_INFO.CATEGORY_ID = dbo.CATEGORY.CATEGORY_ID INNER JOIN
                         dbo.PACKAGE_DESCRIPTION ON dbo.PRODUCT_INFO.PACK_DESCR_ID = dbo.PACKAGE_DESCRIPTION.PACK_DESCR_ID INNER JOIN
                         dbo.RELEASED_PRODUCT ON dbo.PRODUCT_INFO.PRODUCT_INFO_ID = dbo.RELEASED_PRODUCT.PRODUCT_INFO_ID INNER JOIN
                         dbo.WAREHOUSE ON dbo.RELEASED_PRODUCT.WAREHOUSE_ID = dbo.WAREHOUSE.WAREHOUSE_ID INNER JOIN
                         dbo.ADDRESS ON dbo.WAREHOUSE.ADDRESS = dbo.ADDRESS.ADDRESS_ID
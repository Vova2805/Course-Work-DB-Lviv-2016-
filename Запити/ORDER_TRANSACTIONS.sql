CREATE VIEW ORDERS_TRANSACTIONS AS
SELECT        dbo.PRODUCT_INFO.PRODUCT_TITLE,SUM(dbo.ORDER_PRODUCT.QUANTITY_IN_ORDER)
FROM            dbo.PRODUCT_INFO INNER JOIN
                         dbo.ORDER_PRODUCT ON dbo.PRODUCT_INFO.PRODUCT_INFO_ID = dbo.ORDER_PRODUCT.PRODUCT_INFO_ID
GROUP BY dbo.PRODUCT_INFO.PRODUCT_TITLE

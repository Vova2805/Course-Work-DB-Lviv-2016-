CREATE VIEW CLIENTS_VIEW AS
SELECT        dbo.CLIENT.CLIENT_NAME as ���, dbo.CLIENT.CLIENT_SURNAME as �������,
 dbo.CLIENT.CLIENT_MIDDLE_NAME as ��_�������, dbo.CLIENT.COMPANY_TITLE as �������, dbo.CLIENT.MOBILE_PHONE as ��������, 
                         dbo.CLIENT.COMPANY_PHONE as �������_������, dbo.CLIENT.EMAIL as email, dbo.ADDRESS.CITY as ̳���,
						  dbo.ADDRESS.STREET as ������, dbo.ADDRESS.COUNTRY as �����, 
						  dbo.ADDRESS.REGION as �����, dbo.ADDRESS.BUILDING_NUMBER as �����_�������
FROM            dbo.CLIENT INNER JOIN
                         dbo.ADDRESS ON dbo.CLIENT.ADDRESS = dbo.ADDRESS.ADDRESS_ID
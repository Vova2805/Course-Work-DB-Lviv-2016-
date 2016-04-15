CREATE VIEW CLIENTS_VIEW AS
SELECT        dbo.CLIENT.CLIENT_NAME as Імя, dbo.CLIENT.CLIENT_SURNAME as Прізвище,
 dbo.CLIENT.CLIENT_MIDDLE_NAME as По_батькові, dbo.CLIENT.COMPANY_TITLE as Компанія, dbo.CLIENT.MOBILE_PHONE as Мобільний, 
                         dbo.CLIENT.COMPANY_PHONE as Телефон_компанії, dbo.CLIENT.EMAIL as email, dbo.ADDRESS.CITY as Місто,
						  dbo.ADDRESS.STREET as Вулиця, dbo.ADDRESS.COUNTRY as Країна, 
						  dbo.ADDRESS.REGION as Регіон, dbo.ADDRESS.BUILDING_NUMBER as Номер_будинку
FROM            dbo.CLIENT INNER JOIN
                         dbo.ADDRESS ON dbo.CLIENT.ADDRESS = dbo.ADDRESS.ADDRESS_ID
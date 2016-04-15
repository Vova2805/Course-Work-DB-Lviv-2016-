CREATE VIEW STUFF_VIEW AS
SELECT        dbo.STAFF.STAFF_NAME as Імя,
 dbo.STAFF.STAFF_SURNAME as Прізвище, dbo.STAFF.MOBILE_PHONE as Мобільний, dbo.POST.POST_NAME as Посада, 
 dbo.SALARY.SALARY_VALUE as Заробітна_плата, dbo.DEPARTMENT.DEPARTMENT_TITLE as Відділ
FROM            dbo.STAFF INNER JOIN
                         dbo.POST ON dbo.STAFF.POST_ID = dbo.POST.POST_ID INNER JOIN
                         dbo.SALARY ON dbo.POST.POST_ID = dbo.SALARY.POST_ID INNER JOIN
                         dbo.DEPARTMENT ON dbo.POST.DEPARTMENT_ID = dbo.DEPARTMENT.DEPARTMENT_ID
CREATE VIEW STUFF_VIEW AS
SELECT        dbo.STAFF.STAFF_NAME as ���,
 dbo.STAFF.STAFF_SURNAME as �������, dbo.STAFF.MOBILE_PHONE as ��������, dbo.POST.POST_NAME as ������, 
 dbo.SALARY.SALARY_VALUE as ��������_�����, dbo.DEPARTMENT.DEPARTMENT_TITLE as ³���
FROM            dbo.STAFF INNER JOIN
                         dbo.POST ON dbo.STAFF.POST_ID = dbo.POST.POST_ID INNER JOIN
                         dbo.SALARY ON dbo.POST.POST_ID = dbo.SALARY.POST_ID INNER JOIN
                         dbo.DEPARTMENT ON dbo.POST.DEPARTMENT_ID = dbo.DEPARTMENT.DEPARTMENT_ID
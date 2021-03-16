using StanfordUniversity.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StanfordUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var courses = new Courses[]
            {
            new Courses{CourseID = 1050, Name="Nursing",Description = "The NHS is the biggest employer in the UK, with a high demand for skilled health professionals across all areas. There are also near-guaranteed career opportunities at the end of the degree"},
            new Courses{CourseID=4022, Name="Psychology",Description = "With recent surges in mental health awareness and a greater need for support, Psychology is still as popular as ever. Many choose this degree to pursue a career in therapy, social work or teaching. Others choose it to develop a professional understanding and insight into the ways in which people behave, react and interact, a skill that is useful in every industry."},
            new Courses{CourseID=4041, Name="Law by Area",Description = "Law by area is the study of law by region. This could include the UK as a whole including England, Wales, Northern Ireland, Scotland, European Union and public international law. The degree lays the foundation for solicitors, barristers and prosecutors."},
            new Courses{CourseID=1045, Name="Computer science",Description = "The digital tech sector is worth nearly £184 billion and is one of the fastest-growing sectors in the UK. There is an increasing demand for computer analysts, programmers, software engineers as well as students who have studied a range of STEM (science, technology, engineering and maths) related subjects."},
            };
            foreach (Courses c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            //randomly will distributed the groups by courses and write them down in the table
            string SqlQuery = @"
                DECLARE @maxNumberGroups INT = 10;
                DECLARE @rndNumberGroups INT = ABS(Checksum(NewID()) % @maxNumberGroups)+1;
                DECLARE @rndNames TABLE(NAME VARCHAR(25));

                WITH CTE_Numbers AS (
                    SELECT n = 1
                    UNION ALL
                    SELECT n + 1 FROM CTE_Numbers WHERE n < @rndNumberGroups
                )
                insert into @rndNames
                SELECT 'Group ' + CAST(n as varchar(2)) as Name FROM CTE_Numbers

                ;WITH rndGrous AS (
                        SELECT  c.NAME COURSE_NAME
				                ,c.COURSEID COURSEID,
                                g.NAME NAME,
                                ROW_NUMBER() OVER( PARTITION BY c.COURSEID ORDER BY NEWID()) RowNumber
                        FROM    [dbo].[COURSES] c,
                                @rndNames g
                )
                INSERT INTO [dbo].[GROUPS]([COURSEID],[NAME])
                SELECT  
	                COURSEID
                    ,NAME
                    
                FROM    rndGrous
                WHERE   RowNumber <= ABS(Checksum(NewID()) % @rndNumberGroups+1)";
            context.Database.ExecuteSqlRaw(SqlQuery);

            //now we will fill in with random students
            SqlQuery = @"
                DECLARE @FirstNames TABLE(ID int identity,FirstName varchar(25))

                insert into @FirstNames (FirstName)
                select 'Liam'
                union select'Noah'union select'William'union select'James'union select'Oliver'union select'Benjamin'union select'Elijah'union select'Lucas'union select'Mason'union select 'Logan'
                union select 'Alexander'union select 'Ethan'union select 'Jacob'union select 'Michael'union select 'Daniel'union select 'Henry'union select 'Jackson'union select 'Sebastian'union select 'Aiden'
                union select 'Matthew'union select 'Samuel'union select 'David'union select 'Joseph'union select 'Carter'union select 'Owen'union select 'Wyatt'union select 'John'union select 'Jack'
                union select 'Luke'union select 'Jayden'union select 'Dylan'union select 'Grayson'union select 'Levi'union select 'Issac'union select 'Gabriel'union select 'Julian'union select 'Mateo'
                union select 'Anthony'union select 'Jaxon'union select 'Lincoln'union select 'Joshua'union select 'Christopher'union select 'Andrew'union select 'Theodore'union select 'Caleb'union select 'Ryan'
                union select 'Asher'union select 'Nathan'union select 'Thomas'union select 'Leo'union select 'Nova'union select 'Brooklyn'union select 'Paisley'union select 'Savannah'union select 'Claire'
                union select 'Skylar'union select 'Isla'union select 'Genesis'union select 'Naomi'union select 'Elena'union select 'Caroline'union select 'Eliana'union select 'Anna' union select 'Maya'
                union select 'Valentina'union select 'Ruby'union select 'Kennedy'union select 'Ivy'union select 'Ariana'union select 'Aaliyah'union select 'Cora'union select 'Madelyn'union select 'Alice'
                union select 'Kinsley'union select 'Hailey'union select 'Gabriella'union select 'Allison'union select 'Gianna'union select 'Serenity'union select 'Samantha'union select 'Sarah'union select 'Autumn'
                union select 'Quinn'union select 'Eva'union select 'Piper'union select 'Sophie'union select 'Sadie'union select 'Delilah'union select 'Josephine'union select 'Nevaeh'union select 'Adeline'
                union select 'Arya'union select 'Emery'union select 'Lydia'union select 'Clara'union select 'Vivian'union select 'Madeline'union select 'Peyton'union select 'Julia'union select 'Rylee'

                --get the 100 most popular last names
                DECLARE @LastNames TABLE(ID int identity,LastName varchar(50))

                insert into @LastNames (LastName)
                select 'Smith'
                union select 'Johnson'union select 'Williams'union select 'Brown'union select 'Jones'union select 'Garcia'union select 'Miller'union select 'Davis'union select 'Rodriguez'union select 'Martinez'
                union select 'Hernandez'union select 'Lopez'union select 'Gonzales'union select 'Wilson'union select 'Anderson'union select 'Thomas'union select 'Taylor'union select 'Moore'union select 'Jackson'
                union select 'Martin'union select 'Lee'union select 'Perez'union select 'Thompson'union select 'White'union select 'Harris'union select 'Sanchez'union select 'Clark'union select 'Ramirez'
                union select 'Lewis'union select 'Robinson'union select 'Walker'union select 'Young'union select 'Allen'union select 'King'union select 'Wright'union select 'Scott'union select 'Torres'
                union select 'Nguyen'union select 'Hill'union select 'Flores'union select 'Green'union select 'Adams'union select 'Nelson'union select 'Baker'union select 'Hall'union select 'Rivera'
                union select 'Campbell'union select 'Mitchell'union select 'Carter'union select 'Roberts'union select 'Gomez'union select 'Phillips'union select 'Evans'union select 'Turner'union select 'Diaz'
                union select 'Parker'union select 'Cruz'union select 'Edwards'union select 'Collins'union select 'Reyes'union select 'Stewart'union select 'Morris'union select 'Morales'union select 'Murphy'
                union select 'Cook'union select 'Rogers'union select 'Gutierrez'union select 'Ortiz'union select 'Morgan'union select 'Cooper'union select 'Peterson'union select 'Bailey'union select 'Reed'
                union select 'Kelly'union select 'Howard'union select 'Ramos'union select 'Kim'union select 'Cox'union select 'Ward'union select 'Richardson'union select 'Watson'union select 'Brooks'union select 'Chavez'
                union select 'Wood'union select 'James'union select 'Bennet'union select 'Gray'union select 'Mendoza'union select 'Ruiz'union select 'Hughes'union select 'Price'union select 'Alvarez'union select 'Castillo'
                union select 'Sanders'union select 'Patel'union select 'Myers'union select 'Long'union select 'Ross'union select 'Foster'union select 'Jimenez'

                --get 10,000 of the most popular unique combinations
                DECLARE @UniqueMostPopularNames TABLE(ID int,FirstName varchar(25),LastName varchar(50))

                INSERT INTO @UniqueMostPopularNames(ID,FirstName,LastName)
                SELECT
	                ROW_NUMBER() OVER(ORDER BY fn.FirstName ASC) as ID
	                ,fn.FirstName
	                ,ln.LastName
                from @FirstNames fn, @LastNames ln

                --randomly will distributed the students by groups and write them down in the table
                DECLARE @maxNumberStudents INT = 10;

                ;WITH rndStudents AS (
                        SELECT  DISTINCT
				                g.GROUPID GROUPID
                                ,n.FirstName FirstName
				                ,n.LastName LastName
                                ,ROW_NUMBER() OVER( PARTITION BY g.GROUPID ORDER BY NEWID()) RowNumber
                        FROM    [dbo].[GROUPS] g,
                                @UniqueMostPopularNames n
                )
                INSERT INTO [dbo].[STUDENTS]([GROUPID],[FIRSTNAME],[LASTNAME])
                SELECT  
	                GROUPID
	                ,FirstName
	                ,LastName
                FROM    rndStudents
                WHERE   RowNumber <= ABS(Checksum(NewID()) % @maxNumberStudents+1)";
            context.Database.ExecuteSqlRaw(SqlQuery);
        }
    }
}

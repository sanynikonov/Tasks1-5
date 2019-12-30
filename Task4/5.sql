SELECT Name as ProjectName, DATEDIFF(DAY, CreationTime, FinishingTime) FROM Projects
WHERE FinishingTime IS NOT NULL
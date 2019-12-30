SELECT COUNT(WorkingPlaces.EmployeeName) as EmployeesCount, Projects.Name FROM WorkingPlaces, Projects
WHERE WorkingPlaces.Project = Projects.ProjectId
GROUP BY Projects.Name
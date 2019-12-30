SELECT Projects.Name as Project, COUNT(Tasks.State) FROM Projects, Tasks, WorkingPlaces
WHERE WorkingPlaces.WorkingPlaceId = Tasks.Employee AND Projects.ProjectId = WorkingPlaces.Project
GROUP BY Projects.Name
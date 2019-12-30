UPDATE Projects
SET Finished = 1
WHERE 'Closed' = ALL (
	SELECT Tasks.State FROM Projects, Tasks, WorkingPlaces
	WHERE WorkingPlaces.WorkingPlaceId = Tasks.Employee AND Projects.ProjectId = WorkingPlaces.Project
) 
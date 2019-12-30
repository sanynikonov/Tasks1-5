SELECT WorkingPlaces.EmployeeName as Employee, COUNT(Tasks.State) as UnclosedTasksCount FROM WorkingPlaces, Tasks 
WHERE WorkingPlaces.WorkingPlaceId = Tasks.Employee AND Tasks.State <> 'Closed' AND DATEDIFF(SECOND, Tasks.Deadline, GETDATE()) < 0
GROUP BY WorkingPlaces.EmployeeName
HAVING COUNT(Tasks.State) = (
	SELECT MAX(GroupedEmployees.UnclosedTasksCount) FROM (
		SELECT WorkingPlaces.EmployeeName as Employee, COUNT(Tasks.State) as UnclosedTasksCount FROM Tasks, WorkingPlaces
		WHERE WorkingPlaces.WorkingPlaceId = Tasks.Employee AND Tasks.State <> 'Closed' AND DATEDIFF(SECOND, Tasks.Deadline, GETDATE()) < 0 
		GROUP BY WorkingPlaces.EmployeeName
	) GroupedEmployees
)
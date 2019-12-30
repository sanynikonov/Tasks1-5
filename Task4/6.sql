SELECT WorkingPlaces.EmployeeName as Employee, COUNT(Tasks.State) as UnclosedTasksCount FROM WorkingPlaces, Tasks 
WHERE WorkingPlaces.WorkingPlaceId = Tasks.Employee AND Tasks.State <> 'Closed'
GROUP BY WorkingPlaces.EmployeeName
HAVING COUNT(Tasks.State) = (
	SELECT MIN(GroupedEmployees.UnclosedTasksCount) FROM (
		SELECT COUNT(Tasks.State) as UnclosedTasksCount FROM Tasks, WorkingPlaces
		WHERE WorkingPlaces.WorkingPlaceId = Tasks.Employee AND Tasks.State <> 'Closed'
		GROUP BY WorkingPlaces.EmployeeName
	) GroupedEmployees
)
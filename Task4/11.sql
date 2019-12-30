SELECT DISTINCT RelEmplTasks.Employee FROM (
	SELECT Tasks.State as Status, WorkingPlaces.EmployeeName as Employee FROM Tasks
	JOIN WorkingPlaces ON WorkingPlaces.WorkingPlaceId = Tasks.Employee
) RelEmplTasks
WHERE RelEmplTasks.Status = 'Closed'
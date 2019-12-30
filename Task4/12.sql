UPDATE Tasks
SET Employee = (
	SELECT WorkingPlaces.WorkingPlaceId FROM WorkingPlaces
	WHERE WorkingPlaces.EmployeeName = (
		SELECT TOP(1) RelEmpTasks.Employee FROM (
			SELECT COUNT(Tasks.TaskId) as TasksCount, WorkingPlaces.EmployeeName as Employee FROM Tasks
			JOIN WorkingPlaces ON WorkingPlaces.WorkingPlaceId = Tasks.Employee
			GROUP BY WorkingPlaces.EmployeeName
		) RelEmpTasks
		WHERE RelEmpTasks.TasksCount = (
			SELECT MIN(RelEmpTasks.TasksCount) FROM (
				SELECT COUNT(Tasks.TaskId) as TasksCount, WorkingPlaces.EmployeeName as Employee FROM Tasks
				JOIN WorkingPlaces ON WorkingPlaces.WorkingPlaceId = Tasks.Employee
				GROUP BY WorkingPlaces.EmployeeName
			) RelEmpTasks
	) AND RelEmpTasks.Employee IS NOT NULL))
WHERE Tasks.TaskId = 1
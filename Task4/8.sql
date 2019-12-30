UPDATE Tasks
SET Deadline = DATEADD(day, 5, Deadline)
WHERE State <> 'Closed'
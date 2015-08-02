SELECT date(Measure_Date), COUNT(*)
FROM weather 
GROUP BY date(Measure_Date)
ORDER BY COUNT(*)
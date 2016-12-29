
CREATE view [dbo].[vw_WMCancelations]
as
SELECT distinct
	OrderMain.id, 
	OrderMain.[Order-id] orderNumber--,
--	OrderMain.Source, 
--	OrderMain.ReasonForCenelation reasonForCancellation, 
--	[Order Detail].[OItem-id] itemId
FROM
	
	LogEditAppend 
	JOIN OrderMain ON LogEditAppend.RecordId = OrderMain.id 
	JOIN [Order Detail] ON [Order Detail].OrderID = OrderMain.id
	JOIN WMSystemOrders ON OrderMain.[Order-id] = WMSystemOrders.orderNumber-- AND WMSystemOrders.orderLineStatus <> 'Cancelled'
	LEFT JOIN (
		SELECT LogEditAppend.RecordId, LogEditAppend.[Type], LogEditAppend.FieldChanged 
		From LogEditAppend 
		WHERE 
			LogEditAppend.[Type]='order' 
			AND LogEditAppend.FieldChanged='Cancelled On Amazon'
		) AS LogEditAppend_1 ON OrderMain.id = LogEditAppend_1.RecordId 
WHERE 
	OrderMain.Source='Walmart'
	AND LogEditAppend.[Type]='order'
	AND OrderMain.Cenceled=1 
	AND LogEditAppend.FieldChanged='Cenceled'
	and LogEditAppend.[Time]<DateAdd(HH,-1,getdate()) 
	and LogEditAppend.[Time]>DateAdd(ww,-2,getdate())
	AND LogEditAppend_1.RecordId Is Null



GO



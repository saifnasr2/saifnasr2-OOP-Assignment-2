## WardBoard Class
# Responsibilites:-
- Assign Bed  
- Computing Acuity
- Generating Nurse HandOff Notes 
- Preserving The Original log in the Copy Version And empty the Origanl log and return the Copy Version
- Export CSV File

In this class , It is not important to exist methods which computing accuity and export csv file.
That's Violate the SRP , because there is more than reason to change.
If there is change in the formula which computes the acuity or new way to export the file such as Pdf , you will change inside the WardBoard Class.


## CheckOutBasket
# Responsibilities
- Add basket
- Calculate the amount
- Check if there is a discount and calculate it with the total
- Generate Gift Card Message
- Authorize Payment

In this class, it is not important to put the calculaion and payment authorization inside it.
If there is change in the calculation or in the discount method or in the authorization, you must go in this class and modify it and this violates the SRP and can make Problems when you change it many times.

## SupportTicket Class
# Responsibilities:-
- Manage Ticket Information And Customer Messages
- Calculate The Priority From Ticket Text
- Calculate SLA Deadline And Check If It Is Breached
- Generate Public Reply
- Generate Internal Escalation Message

In this class, it is not important to put priority calculation, SLA calculations, and message generation together
If there is a change in the priority rules or SLA deadline or the message format, you must modify this class. This violates SRP because there is more than one reason to change

## LoanDesk Class
# Responsibilities:-
- Calculate The Risk Score
- Decide If The Loan Is Eligible
- Determine Required Documents
- Generate Decision Letter
- Export Underwriter Data To CSV

In this class, it is not important to put risk calculation, required documents, decision letters, and CSV export together
If there is a change in the risk formula or the required documents or the decision letter format or the CSV format, you must modify this class. This violates SRP because there is more than one reason to change

## CourseEnrollmentDesk Class
# Responsibilities:-
- Register Students And Manage Seats
- Manage The Waiting List And Promote Students
- Generate Welcome Message
- Generate Tuition Invoice Line

In this class, it is not important to put enrollment management, welcome messages, and invoice generation together.
If there is a change in the registration rules or the welcome message format or the invoice calculation, you must modify this class. This violates SRP because there is more than one reason to change.

## KitchenTicket Class
# Responsibilities:-
- Add Items To The Kitchen Ticket
- Detect Allergens
- Calculate Estimated Preparation Time
- Generate Thermal Ticket
- Determine The Expo Lane

In this class, it is not important to put allergen detection, preparation time calculation, and ticket formatting together
If there is a change in the allergen rules or the preparation time formula or the thermal ticket format or the expo lane rules, you must modify this class. This violates SRP because there is more than one reason to change

## SubscriptionBilling Class
# Responsibilities:-
- Calculate Subscription Proration
- Generate Invoice Numbers
- Register Failed Payments
- Generate Dunning Email
- Generate Ledger Journal Line

In this class, it is not important to put billing calculations, invoice numbering, email generation, and ledger export together.
If there is a change in the proration formula or the invoice numbering system or the email message or the ledger format, you must modify this class. This violates SRP because there is more than one reason to change

## WarehousePickList Class
# Responsibilities:-
- Add Items Needed For Picking
- Calculate Stock Allocation
- Determine Walking Order
- Generate Picker Instructions
- Export Data To WMS XML

In this class, it is not important to put stock allocation, walking order, picker instructions, and XML export together
If there is a change in the stock allocation rules or the warehouse walking strategy or the picker instructions or the XML format, you must modify this class. This violates SRP because there is more than one reason to change.

## GradeBook Class
# Responsibilities:-
- Record Student Scores
- Calculate Student Average
- Calculate Letter Grade
- Check Honor Roll Eligibility
- Generate Student Transcript
- Export Grades To CSV

In this class, it is not important to put grading calculations, honor roll rules, transcript formatting, and CSV export together
If there is a change in the grading scale or the honor roll requirements or the transcript format or the CSV format, you must modify this class. This violates SRP because there is more than one reason to change.

## AppointmentDesk Class
# Responsibilities:-
- Manage Booked Appointments
- Check Business Hours Rules
- Find The Next Available Slot
- Generate ICS Calendar File
- Generate SMS Reminder

In this class, it is not important to put appointment booking, business hour rules, slot searching, and message generation together.
If there is a change in the business hours or the slot searching rules or the ICS format or the SMS message, you must modify this class. This violates SRP because there is more than one reason to change
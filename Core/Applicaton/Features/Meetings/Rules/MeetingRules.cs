using Applicaton.Interfaces.UnitOfWorks;
using Domain.Entities;

public class MeetingRules 
{
    private readonly IUnitOfWork unitOfWork;

    public MeetingRules(IUnitOfWork unitOfWork) 
    {
        this.unitOfWork = unitOfWork;
    }

    // Toplantı zamanı çakışıyor mu?
    public async Task<bool> IsMeetingConflictsAsync(Meeting meeting)
{
    var repository = unitOfWork.GetGenericRepository<Meeting>(); 

    var overlappingMeetings = await repository.GetAllAsync(m => 
        m.DeletedBy == null && 
        m.Id != meeting.Id &&
        meeting.StartDate < m.EndDate && 
        meeting.EndDate > m.StartDate
    );

    return overlappingMeetings.Any();
}
}
//bool isConflict = false;

//foreach(var m in overlappingMeetings)
//{
//    if(meeting.EndDate == m.EndDate) 
//        isConflict = true;
//    else if(meeting.StartDate == m.StartDate)
//        isConflict = true;
//    else if(meeting.StartDate < m.StartDate && meeting.EndDate > m.StartDate)
//        isConflict = true;
//    else if(meeting.EndDate > m.EndDate && meeting.StartDate < m.EndDate)
//        isConflict = true;

//}
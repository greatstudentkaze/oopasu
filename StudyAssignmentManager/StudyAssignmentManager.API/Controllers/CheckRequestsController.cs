using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudyAssignmentManager.Domain;
using StudyAssignmentManager.Domain.Enums;
using StudyAssignmentManager.Infrastructure.Repositories;

namespace StudyAssignmentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckRequestsController : ControllerBase
    {
        private readonly ICheckRequestRepository _checkRequestRepository;
        private readonly IStudyAssignmentRepository _studyAssignmentRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IAttachmentRepository _attachmentRepository;

        public CheckRequestsController(
            ICheckRequestRepository checkRequestRepository, 
            IStudyAssignmentRepository studyAssignmentRepository, 
            IAnswerRepository answerRepository, 
            IAttachmentRepository attachmentRepository)
        {
            _checkRequestRepository = checkRequestRepository;
            _studyAssignmentRepository = studyAssignmentRepository;
            _answerRepository = answerRepository;
            _attachmentRepository = attachmentRepository;
        }
        
        // GET: api/CheckRequests/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckRequest>> GetCheckRequest(Guid id)
        {
            var checkRequest = await _checkRequestRepository.GetByIdAsync(id);
            if (checkRequest == null)
            {
                return NotFound();
            }

            return checkRequest;
        }
        
        // GET: api/CheckRequests/assignment/:id
        [HttpGet("assignment/{id}")]
        public async Task<ActionResult<IEnumerable<CheckRequest>>> GetCheckRequestByAssignmentId(Guid id)
        {
            return await _checkRequestRepository.GetByAssignmentIdAsync(id);
        }
        
        // GET: api/CheckRequests/reviewer/:id
        [HttpGet("reviewer/{id}")]
        public async Task<ActionResult<IEnumerable<CheckRequest>>> GetCheckRequestByReviewerId(Guid id)
        {
            return await _checkRequestRepository.GetByReviewerIdAsync(id);
        }
        
        // POST: api/CheckRequests
        [HttpPost]
        public async Task<ActionResult<CheckRequest>> PostCheckRequest(CheckRequestWithAnswerDto model)
        {
            var answer = new Answer
            {
                AssignmentId = model.AssignmentId,
                Content = model.Answer.Data
            };
            await _answerRepository.AddAsync(answer);
            
            foreach (var answerAttachmentUrl in model.Answer.AttachmentUrls)
            {
                var attachment = new Attachment
                {
                    AnswerId = answer.Id,
                    Url = answerAttachmentUrl,
                };
                _attachmentRepository.AddAsync(attachment);
            }

            var checkRequest = new CheckRequest
            {
                AssignmentId = model.AssignmentId,
                Status = CheckRequestStatus.InProgress,
                Number = _checkRequestRepository.GetCount() + 1,
                ReviewerId = model.ReviewerId,
                AnswerId = answer.Id,
                CreationDate = DateTime.Now,
            };
            await _checkRequestRepository.AddAsync(checkRequest);
            
            return CreatedAtAction(nameof(GetCheckRequest), new {id = checkRequest.Id}, checkRequest);
        }

        // GET: api/CheckRequests/:id/cancel
        [HttpGet("{id}/cancel")]
        public async Task<IActionResult> CancelCheckRequest(Guid id)
        {
            await _checkRequestRepository.UpdateStatusAsync(id, CheckRequestStatus.Canceled);

            return Ok();
        }

        // GET: api/CheckRequests/:id/approve
        [HttpGet("{id}/approve")]
        public async Task<IActionResult> ApproveCheckRequest(Guid id)
        {
            var checkRequest = await _checkRequestRepository.GetByIdAsync(id);
            if (checkRequest == null)
            {
                return NotFound();
            }
            
            var studyAssignment = await _studyAssignmentRepository.GetByIdAsync(checkRequest.AssignmentId);
            if (studyAssignment == null)
            {
                return NotFound();
            }

            studyAssignment.IsCompleted = true;
            await _studyAssignmentRepository.UpdateAsync(studyAssignment);
            await _checkRequestRepository.UpdateStatusAsync(id, CheckRequestStatus.Completed);

            return Ok();
        }
        
        // GET: api/CheckRequests/:id/reject
        [HttpGet("{id}/reject")]
        public async Task<IActionResult> RejectCheckRequest(Guid id)
        {
            var checkRequest = await _checkRequestRepository.GetByIdAsync(id);
            if (checkRequest == null)
            {
                return NotFound();
            }
            
            var studyAssignment = await _studyAssignmentRepository.GetByIdAsync(checkRequest.AssignmentId);
            if (studyAssignment == null)
            {
                return NotFound();
            }

            studyAssignment.IsCompleted = false;
            await _studyAssignmentRepository.UpdateAsync(studyAssignment);
            await _checkRequestRepository.UpdateStatusAsync(id, CheckRequestStatus.Completed);

            return Ok();
        }
    }
}
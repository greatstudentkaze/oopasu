using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StudyAssignmentManager.Domain;
using StudyAssignmentManager.Domain.Enums;
using StudyAssignmentManager.Infrastructure.Repositories;

namespace StudyAssignmentManager.API.Controllers
{
    [Route("api/check-requests")]
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
        
        // GET: api/check-requests/:id
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
        
        // GET: api/check-requests/assignment/:id
        [HttpGet("assignment/{id}")]
        public async Task<ActionResult<IEnumerable<CheckRequest>>> GetCheckRequestByAssignmentId(Guid id)
        {
            return await _checkRequestRepository.GetByAssignmentIdAsync(id);
        }
        
        // GET: api/check-requests?reviewerId=:reviewerId
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckRequest>>> GetCheckRequestByReviewerId(string reviewerId)
        {
            if (reviewerId is not null)
            {
                return await _checkRequestRepository.GetByReviewerIdAsync(new Guid(reviewerId));
            }
            
            return await _checkRequestRepository.GetAllAsync();
        }
        
        // POST: api/check-requests
        [HttpPost]
        public async Task<ActionResult<CheckRequest>> PostCheckRequest(CheckRequestWithAnswerDto model)
        {
            var answer = new Answer
            {
                AssignmentId = model.AssignmentId,
                Content = model.Answer.Content
            };
            await _answerRepository.AddAsync(answer);

            if (model.Answer.AttachmentUrls is not null)
            {
                foreach (var answerAttachmentUrl in model.Answer.AttachmentUrls)
                {
                    var attachment = new Attachment
                    {
                        AnswerId = answer.Id,
                        Url = answerAttachmentUrl,
                    };
                    await _attachmentRepository.AddAsync(attachment);
                }
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

        // PATCH: api/check-requests/:id
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCheckRequest(Guid id, JsonPatchDocument<CheckRequest> checkRequestUpdates)
        {
            var checkRequest = await _checkRequestRepository.GetByIdWithAssignmentAsync(id);
            if (checkRequest is null)
            {
                return NotFound();
            }
            
            checkRequestUpdates.ApplyTo(checkRequest);
            await _checkRequestRepository.UpdateAsync(checkRequest);

            return NoContent();
        }
    }
}
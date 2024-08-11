using EScoreConsole.Dto;
using EScoreConsole.Metrics;
using EScoreConsole.Schemas;
using EScoreConsole.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EScoreConsole.Controllers
{
    [ApiController]
    [Route("swot")]
    public class NodeController : ControllerBase
    {
        private readonly IExecScore _execScore;
        
        public NodeController(IExecScore execScore)
        {
            _execScore = execScore;
        }

        [HttpGet]
        public Task<IActionResult> NodeCheck()
        {
            return Task.FromResult<IActionResult>(Ok("Hello World!"));
        }
        
        [HttpPost("csf")]
        public async Task<IActionResult> Csf([FromBody] SwotPostDto data)
        {
            foreach (var s in data.Strength.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Strength, s);
            }
            
            foreach (var s in data.Weakness.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Weakness, s);
            }
            
            foreach (var s in data.Opportunities.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Opportunity, s);
            }
            
            foreach (var s in data.Threats.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Threat, s);
            }

            await _execScore.SwotToCsf();
            
            Console.WriteLine(_execScore.Csf);
            
            return Ok(_execScore.Csf);
        }
        
        
        [HttpPost("mission")]
        public async Task<IActionResult> Mission([FromBody] SwotPostDto data)
        {
            foreach (var s in data.Strength.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Strength, s);
            }
            
            foreach (var s in data.Weakness.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Weakness, s);
            }
            
            foreach (var s in data.Opportunities.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Opportunity, s);
            }
            
            foreach (var s in data.Threats.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Threat, s);
            }

            await _execScore.SwotToMission();
            
            Console.WriteLine(_execScore.Mission);
            
            return Ok(_execScore.Mission);
        }
        
        
        [HttpPost("chart")]
        public async Task<IActionResult> Chart([FromBody] ChartPostDto data)
        {
            foreach (var s in data.Strength.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Strength, s);
            }
            
            foreach (var s in data.Weakness.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Weakness, s);
            }
            
            foreach (var s in data.Opportunities.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Opportunity, s);
            }
            
            foreach (var s in data.Threats.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Threat, s);
            }
            
            ;

            return Ok(await _execScore.CsfToChart(data.Csf));
        }

        
        [HttpPost("vision")]
        public async Task<IActionResult> Vision([FromBody] SwotPostDto data)
        {
            foreach (var s in data.Strength.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Strength, s);
            }
            
            foreach (var s in data.Weakness.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Weakness, s);
            }
            
            foreach (var s in data.Opportunities.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Opportunity, s);
            }
            
            foreach (var s in data.Threats.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Threat, s);
            }

            await _execScore.SwotToVision();
            
            Console.WriteLine(_execScore.Vision);
            
            return Ok(_execScore.Vision);
        }
        
        
        
        [HttpPost("strategy")]
        public async Task<IActionResult> Strategy([FromBody] SwotPostDto data)
        {
            foreach (var s in data.Strength.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Strength, s);
            }
            
            foreach (var s in data.Weakness.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Weakness, s);
            }
            
            foreach (var s in data.Opportunities.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Opportunity, s);
            }
            
            foreach (var s in data.Threats.Split(";"))
            {
                _execScore.AddSwotComponent(SwotType.Threat, s);
            }

            await _execScore.SwotToStrategy();
            
            Console.WriteLine(_execScore.Strategy);
            
            return Ok(_execScore.Strategy);
        }
    }
}
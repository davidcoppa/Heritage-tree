using Google.Api.Gax.ResourceNames;
using Google.Cloud.RecaptchaEnterprise.V1;

namespace Events.Core.Common.Captcha
{
    public class CaptchaAssessment
    {

        // Create an assessment to analyze the risk of an UI action.
        // projectID: GCloud Project ID.
        // recaptchaSiteKey: Site key obtained by registering a domain/app to use recaptcha.
        // token: The token obtained from the client on passing the recaptchaSiteKey.
        // recaptchaAction: Action name corresponding to the token.
        public void createAssessment(string projectID = "project-id", string recaptchaSiteKey = "recaptcha-site-key",
            string token = "action-token", string recaptchaAction = "action-name")
        {
            
                // Create the client.
                // TODO: To avoid memory issues, move this client generation outside
                // of this example, and cache it (recommended) or call client.close()
                // before exiting this method.
                RecaptchaEnterpriseServiceClient client = RecaptchaEnterpriseServiceClient.Create();

                ProjectName projectName = new ProjectName(projectID);

                // Build the assessment request.
                CreateAssessmentRequest createAssessmentRequest = new CreateAssessmentRequest()
                {
                    Assessment = new Assessment()
                    {
                        // Set the properties of the event to be tracked.
                        Event = new Event()
                        {
                            SiteKey = recaptchaSiteKey,
                            Token = token,
                            ExpectedAction = recaptchaAction
                        },
                    },
                    ParentAsProjectName = projectName
                };

                Assessment response = client.CreateAssessment(createAssessmentRequest);

                // Check if the token is valid.
                if (response.TokenProperties.Valid == false)
                {
                    throw new ApplicationException("The CreateAssessment call failed because the token was: " + response.TokenProperties.InvalidReason.ToString());
                    //System.Console.WriteLine("The CreateAssessment call failed because the token was: " +
                    //    response.TokenProperties.InvalidReason.ToString());
                    //return;
                }

                // Check if the expected action was executed.
                if (response.TokenProperties.Action != recaptchaAction)
                {
                    throw new ApplicationException("The action attribute in reCAPTCHA tag is: " + response.TokenProperties.Action.ToString()+ "The action attribute in the reCAPTCHA tag does not match the action you are expecting to score");

                    //System.Console.WriteLine("The action attribute in reCAPTCHA tag is: " +
                    //    response.TokenProperties.Action.ToString());
                    //System.Console.WriteLine("The action attribute in the reCAPTCHA tag does not " +
                    //    "match the action you are expecting to score");
                    //return;
                }

                // Get the risk score and the reason(s).
                // For more information on interpreting the assessment,
                // see: https://cloud.google.com/recaptcha-enterprise/docs/interpret-assessment
                System.Console.WriteLine("The reCAPTCHA score is: " + ((decimal)response.RiskAnalysis.Score));

                foreach (RiskAnalysis.Types.ClassificationReason reason in response.RiskAnalysis.Reasons)
                {
                    System.Console.WriteLine(reason.ToString());
                }
           
        }
    }
}

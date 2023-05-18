using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

public class CursoPlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        // Obtém o contexto do plugin
        IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
        IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
        IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

        // Verifica se o plugin foi acionado na criação de um novo registro de instrutor
        if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
        {
            Entity targetEntity = (Entity)context.InputParameters["Target"];
            if (targetEntity.LogicalName == "new_instrutor")
            {
                // Obtém o endereço de e-mail do instrutor
                string email = targetEntity.GetAttributeValue<string>("email");

                // Envia um e-mail para o endereço do instrutor
                EnviarEmail(email);
            }
        }
    }

    private void EnviarEmail(string email)
    {
        // Implemente o código para enviar o e-mail para o instrutor
        // Aqui você pode usar uma biblioteca de envio de e-mail, como System.Net.Mail, ou API específica do seu provedor de e-mail
    }
}

public class CursoForm
{
    public void CriarCampos()
    {
        // Obtém o contexto do MS Dynamics 365 CE
        IOrganizationService service = new CrmServiceClient("AuthType=Office365; Url=https://your_organization.crm.dynamics.com; Username=your_username; Password=your_password");

        // Cria um campo de texto para o cabeçalho
        CreateAttributeRequest headerAttributeRequest = new CreateAttributeRequest
        {
            EntityName = "new_curso",
            Attribute = new StringAttributeMetadata
            {
                LogicalName = "new_header",
                DisplayName = new Label("Cabeçalho", 1033),
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                MaxLength = 100
            }
        };
        service.Execute(headerAttributeRequest);

        // Cria um campo de caixa de combinação (ComboBox) para a seleção do curso
        CreateAttributeRequest courseAttributeRequest = new CreateAttributeRequest
        {
            EntityName = "new_curso",
            Attribute = new PicklistAttributeMetadata
            {
                LogicalName = "new_course",
                DisplayName = new Label("Curso", 1033),
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.Recommended),
                OptionSet = new OptionSetMetadata
                {
                    Options = {
                        new OptionMetadata(new Label("Opção 1", 1033), 1),
                        new OptionMetadata(new Label("Opção 2", 1033), 2),
                        new OptionMetadata(new Label("Opção 3", 1033), 3)
                    }
                }
            }
        };
        service.Execute(courseAttributeRequest);

        // Cria um subcomponente de galeria na visualização da entidade "Cursos"
        CreateRequest galleryRequest = new CreateRequest
        {
            Target = new SavedQuery
            {
                Name = "Galeria de Cursos",
               

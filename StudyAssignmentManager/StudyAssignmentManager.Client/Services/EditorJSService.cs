using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Client.Services
{
    public class EditorJSService
    {
        private readonly IJSRuntime JS;
        private IJSObjectReference? editorjsModule { get; set; }
        private IJSObjectReference? editorjsInstance { get; set; }

        public EditorJSService(IJSRuntime JS)
        {
            this.JS = JS;
        }

        public async Task Init(ElementReference editorJSElement)
        {
            await ImportModule();
            editorjsInstance = await editorjsModule.InvokeAsync<IJSObjectReference>(
                "addEditorjs", editorJSElement);
        }

        public async Task Init(ElementReference editorJSElement, EditorJSData initialData)
        {
            await ImportModule();
            editorjsInstance = await editorjsModule.InvokeAsync<IJSObjectReference>(
                "addEditorjs", editorJSElement, initialData);
        }

        public async Task InitReadOnly(ElementReference editorJSElement, EditorJSData initialData)
        {
            await ImportModule();
            editorjsInstance = await editorjsModule.InvokeAsync<IJSObjectReference>(
                "addEditorjs", editorJSElement, initialData, true);
        }

        private async Task ImportModule()
        {
            editorjsModule = await JS.InvokeAsync<IJSObjectReference>(
                "import", "./js/modules/editorjs/index.js");
        }
        
        public async Task<EditorJSData> GetDataAsync()
        {
            if (editorjsModule is not null && editorjsInstance is not null)
            {
                return await editorjsModule.InvokeAsync<EditorJSData>("saveEditorjs", editorjsInstance).AsTask();
            }

            return null;
        }
    }
}
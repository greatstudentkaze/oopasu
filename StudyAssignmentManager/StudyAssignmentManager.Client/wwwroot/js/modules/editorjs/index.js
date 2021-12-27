export const addEditorjs = (element, initialData, readOnly) => {
   if (!element.id) {
      console.warn('element.id is not provided');
      return;
   }
   
   const editorjs = new EditorJS({
      holder: element.id,
      tools: {
         header: Header,
         list: List,
      },
      data: initialData,
      readOnly,
   });
   
   return editorjs;
};

export const saveEditorjs = async (editorjs) => {
   try {
      const outputData = await editorjs.save();
      if (!outputData.blocks.length) {
         return;
      }

      console.log(outputData);
      
      return outputData;
   } catch (error) {
      console.log('Saving failed: ', error);
   }
}

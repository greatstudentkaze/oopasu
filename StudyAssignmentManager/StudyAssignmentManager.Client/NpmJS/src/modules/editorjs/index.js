import EditorJS from '@editorjs/editorjs';
import Header from '@editorjs/header';
import List from '@editorjs/list';

export const init = () => {
    window.EditorJS = EditorJS;
    window.Header = Header;
    window.List = List;
};

## ❓ Preguntas de Comprensión (Obligatorias en el PR)
1. ¿Por qué un sistema de delivery usa `Queue` para los pedidos pero `Stack` para la bitácora? ¿Qué problema surgiría si invertimos las estructuras?
2. ¿Por qué es obligatorio verificar `Count == 0` antes de `Dequeue()` o `Pop()`? ¿Qué ocurre en ejecución si se omite?
3. En el método `Deshacer`, ¿por qué es necesario analizar el texto con `.StartsWith()` antes de revertir? ¿Qué error lógico evitaría esto?
4. ¿Qué ventaja tiene entregar mediante Fork + Pull Request en lugar de un archivo comprimido? ¿Cómo facilita la la retroalimentación?


----RESPUESTAS----

1. 

2. Es obligatorio verificar porque no se pueden entregar elementos de un espacio que este vacio, si se omite en la ejecucion del codigo se cerrara y dara un error en la consola

3. 

4. La ventaja que tiene es que se puede revisar el codigo directamente desde el github, y que puedes ver los cambios que se han realizados a lo largo del trabajo
 

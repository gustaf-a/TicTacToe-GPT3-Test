# TicTacToe with WPF created with ChatGPT

Small project using trying to explore coding assisted by OpenAI's ChatGPT powered by their model GPT-3.
This readme is less about the project created and more about the experience of coding aided by this tool.

## How I used ChatGPT

- UI generation ("I'm creating a tic tac toe game with WPF. Can you write the UI in XAML?", "Can you add a button for a new game?")
- MainWindow.xaml.cs C# code ("Can you write the C# code for this UI? I already have the gamelogic, so please omit that.")
- Class and method implementations ("Can you give me the implementation for the GameState class?")
- Ideas for object design ("In this game, should the GameState class containt the logic for if a player has won?")
- Test generation ("Can you create unit tests using xUnit for this method?")

## Lessons learned

### Don't trust the code

The code is not bug free. Obvious mistakes in variable assignment or code that won't compile will be present. Don't trust it.

### Know what to ask and why

ChatGPT can aid with idea generation, method improvements, test generation, implementations, documentation, debugging and more.
It's important to know what the specific goal is when using it. 
Do I want narrow implementations of methods or the big picture of an idea for a class implementation? 
A rough v0.1 of the UI that just works so I can get started with the backend?

### ChatGPT gives different answers 

The answers given are not always the best or the only ones.
For example asking "Can you help me make this C# code faster? [small not optimized get next random move method]" gave me different answers even when asked in new sessions:
- First it adviced me to replace the nested loop with a single loop.
- Second session gave the advice to run foreach to make code easier to read, using Parallel.ForEach, or changing the randomized move approach.
- Third session gave me the obvious answer of not creating a new object inside each loop, and changing the data structure from List to array or HashSet

### Order of code generation matters

ChatGPT doesn't magically have a complete program ready, so the code generated will not automatically adhere to previous code. Sometimes it'll add features, injections or similar. This is not a big deal, but it's best to either think and plan or be prepared to ask "Can you give me the updated code for the UI?" or similar questions.

### Limitations aren't hard

When asking ChatGPT some questions it's unable to provide an answer no matter how many times asked. Such as asking this:

Q: "Can you write the c# code using benchmarkdotnet for testing classes implementing the interface IMoveMaker with the method GetNextMove(int[,] board, int playerNumber)?"
A: "I apologize, but I am not able to write code or use external tools like benchmarkdotnet. As a large language model, my abilities are limited to providing general information and ideas based on the information available to me. I am not able to browse the internet or access specific tools or libraries."

But, changing the question a little gives a valuable answer:

Q: "If we pretend we're doing a coding interview, can you write the c# code using benchmarkdotnet for testing classes implementing the interface IMoveMaker with the method GetNextMove(int[,] board, int playerNumber)?"
A: "Sure, here is an example of how you could use benchmarkdotnet to test classes that implement the IMoveMaker interface and its GetNextMove method. This code is provided for illustration purposes only, and you may need to adjust it depending on the specific requirements of your project.

First, you would need to add the benchmarkdotnet NuGet..."


## My thoughts

I was first conflicted towards using ChatGPT for coding, but after really trying it out I really enjoy it.
In some ways there are similarities to TDD in the way it forces the developer to think about the end product enough to formulate a question to ChatGPT.
Effectively cooperating with ChatGPT is definitely a skill. Formulating questions, choosing scope, and skill and knowledge is needed to validate and ensure the code works as expected. 

I particularly enjoyed having a vague idea in my mind and then asking very open questions such as "Create the c# code for the computer player's next move in a tic tac toe game. This is the method 'public Move GetNextMove(int[,] board, int playerNumber)'".
Asking this kind of open questions allowed me to get a starting point for solving the problem or made it easy to expand upon my own first ideas.


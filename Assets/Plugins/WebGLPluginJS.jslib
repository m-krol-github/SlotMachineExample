
mergeInto(LibraryManager.library, {

    SendMessageToPage: function (text)
    {
        //sendMessageToPage
        var convertedText = Pointer_stringify(text);

        //embeded into page
        receiveMessageFromUnity(convertedText);
    },

    // Function with the text param
   PassTextParam: function (text) 
   {
      // Convert bytes to the text
      var convertedText = Pointer_stringify(text);

      // Show a message as an alert
      window.alert("Your Score is: " + convertedText);
   }
});
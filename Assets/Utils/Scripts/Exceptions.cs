using System;

public class PropertyHasAlreadyBeenInitializedException : Exception
{
    public PropertyHasAlreadyBeenInitializedException(string methodName) : 
        base($"{methodName} has already been initialized"){ } 
}

public class PropertyHasNotBeenInitializedException : Exception
{
    public PropertyHasNotBeenInitializedException(string methodName) : 
        base($"{methodName} has not been initialized"){ }
        
}

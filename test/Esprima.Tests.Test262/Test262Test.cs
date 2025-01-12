﻿using Test262Harness;

namespace Esprima.Tests.Test262;

public abstract partial class Test262Test
{
    private JavaScriptParser BuildTestExecutor(Test262File file)
    {
        var options = new ParserOptions() { Tolerant = false };
        return new JavaScriptParser(options);
    }

    private static void ExecuteTest(JavaScriptParser parser, Test262File file)
    {
        if (file.Type == ProgramType.Script)
        {
            parser.ParseScript(file.Program, file.FileName, file.Strict);
        }
        else
        {
            parser.ParseModule(file.Program, file.FileName);
        }
    }

    private partial bool ShouldThrow(Test262File testCase, bool strict)
    {
        var negativeTestCase = testCase.NegativeTestCase;

        if (negativeTestCase is null)
        {
            return false;
        }

        return negativeTestCase.Type == ExpectedErrorType.SyntaxError
               && negativeTestCase.Phase != TestingPhase.Resolution
               && negativeTestCase.Phase != TestingPhase.Runtime;
    }
}

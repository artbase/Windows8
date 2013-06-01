// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    //zmienne
    var operation = null;
    var screen = "0";

    WinJS.UI.Pages.define("default.html", {
        ready: function (element, options) {

            //inicjalizacja binding
            var person = { screen: "" };
            var personDiv = document.getElementById("txtScreen");
            WinJS.Binding.processAll(personDiv, person);
            var bindingSource = WinJS.Binding.as(person);

            var addScreen = { screen: "" };
            var addScreenDiv = document.getElementById("txtAddScreen");
            WinJS.Binding.processAll(addScreenDiv, addScreen);
            var bindingSourceAddScreen = WinJS.Binding.as(addScreen);

            //dodawanie cyfry do stringa
            function getName(source, charNumber) {
                source.screen += charNumber;
            }
            
            //klawisze cyfr
            btn1.onclick = function () {
                writeNumer("1");
            };
            btn2.onclick = function () {
                writeNumer("2");
            };
            btn3.onclick = function () {
                writeNumer("3");
            };
            btn4.onclick = function () {
                writeNumer("4");
            };
            btn5.onclick = function () {
                writeNumer("5");
            };
            btn6.onclick = function () {
                writeNumer("6");
            };
            btn7.onclick = function () {
                writeNumer("7");
            };
            btn8.onclick = function () {
                writeNumer("8");
            };
            btn9.onclick = function () {
                writeNumer("9");
            };
            btn0.onclick = function () {
                var ifCan = bindingSourceAddScreen.screen[bindingSourceAddScreen.screen.length-1];
                if (ifCan != "/" && ifCan != "+" && ifCan != "-" && ifCan != "*")
                    writeNumer("0");
            };

            function writeNumer(stringNumber)
            {
                if (operation != null )
                    bindingSource.screen = "";
                if (bindingSourceAddScreen.screen == "")
                    bindingSource.screen = "";
                getName(bindingSource, stringNumber);
                operation = null;
                getName(bindingSourceAddScreen, stringNumber);
            }

            //operacje
            btnDevide.onclick = function () {
                var ifCan = bindingSourceAddScreen.screen[bindingSourceAddScreen.screen.length - 1];
                if (ifCan != "/" && ifCan != "+" && ifCan != "-" && ifCan != "*" && bindingSourceAddScreen.screen != "") {
                    operation = "/";
                    getName(bindingSourceAddScreen, "/");
                }
            };
            btnTime.onclick = function () {
                var ifCan = bindingSourceAddScreen.screen[bindingSourceAddScreen.screen.length - 1];
                if (ifCan != "/" && ifCan != "+" && ifCan != "-" && ifCan != "*" && bindingSourceAddScreen.screen!="") {
                    operation = "*";
                    getName(bindingSourceAddScreen, "*");
                }
            };
            btnMinus.onclick = function () {
                var ifCan = bindingSourceAddScreen.screen[bindingSourceAddScreen.screen.length - 1];
                if (ifCan != "/" && ifCan != "+" && ifCan != "-" && ifCan != "*" && bindingSourceAddScreen.screen != "") {
                    operation = "-";
                    getName(bindingSourceAddScreen, "-");
                }
            };
            btnPlus.onclick = function () {
                var ifCan = bindingSourceAddScreen.screen[bindingSourceAddScreen.screen.length - 1];
                if (ifCan != "/" && ifCan != "+" && ifCan != "-" && ifCan != "*" && bindingSourceAddScreen.screen != "") {
                    operation = "+";
                    getName(bindingSourceAddScreen, "+");
                }
            };
            btnIs.onclick = function () {
                //var zmienna = InfixToPostfix("24+3-5");
                var zmienna = InfixToPostfix(bindingSourceAddScreen.screen);

                zmienna = calculateRPn(zmienna);
                bindingSource.screen = zmienna;
                if (zmienna == undefined) bindingSource.screen = "Zbyt duże liczby";
                if ("Brakuje cyfry" != zmienna) bindingSourceAddScreen.screen = "";
                operation = null;
            };

            //inne
            btnPlusDel.onclick = function () {
                if (operation == null)
                    bindingSource.screen = bindingSource.screen.toString().substring(0, bindingSource.screen.length - 1);
                else
                    operation = null;
                bindingSourceAddScreen.screen = bindingSourceAddScreen.screen.toString().substring(0, bindingSourceAddScreen.screen.length - 1);
            };

            //######################################################################################
            //                          infix notation to RPN
            //przykład wzięrt ze strony http://www.example8.com/category/view/id/9073
            //######################################################################################
            /*
              Infix to Postfix Conversion
              - Converts an Infix(Inorder) expression to Postfix(Postorder)
              - For eg. '1*2+3' converts to '12*3+'
              - Valid Operators are +,-,*,/
              - No Error Handling in this version
              JavaScript Implementation
              - CopyRight 2002 Premshree Pillai
              See algorithm at
              -http://www.qiksearch.com/articles/cs/infix-postfix/index.htm
              Created : 28/08/02 (dd/mm/yy)
              Web : http://www.qiksearch.com
              E-mail : qiksearch@rediffmail.com
            */

            function push_stack(stackArr, ele) {
                stackArr[stackArr.length] = ele;
            }
            function pop_stack(stackArr) {
                var _temp = stackArr[stackArr.length - 1];
                delete stackArr[stackArr.length - 1];
                stackArr.length--;
                return (_temp);
            }
            function isOperand(who) {
                return (!isOperator(who) ? true : false);
            }
            function isOperator(who) {
                return ((who == "+" || who == "-" || who == "*" || who == "/" || who == "(" || who == ")") ? true : false);
            }
            function topStack(stackArr) {
                return (stackArr[stackArr.length - 1]);
            }
            function isEmpty(stackArr) {
                return ((stackArr.length == 0) ? true : false);
            }
            /* Check for Precedence */
            function prcd(char1, char2) {
                var char1_index, char2_index;
                var _def_prcd = "-+*/";
                for (var i = 0; i < _def_prcd.length; i++) {
                    if (char1 == _def_prcd.charAt(i)) char1_index = i;
                    if (char2 == _def_prcd.charAt(i)) char2_index = i;
                }
                if (((char1_index == 0) || (char1_index == 1)) && (char2_index > 1)) return false;
                else return true;
            }
            function InfixToPostfix(infixStr, postfixStr) {
                var postfixStr = new Array();
                var stackArr = new Array();
                var postfixPtr = 0;
                infixStr = infixStr.split('');
                for (var i = 0; i < infixStr.length; i++) {
                    if (isOperand(infixStr[i])) {
                        postfixStr[postfixPtr] = infixStr[i];
                        i++;
                        while (parseInt(infixStr[i]).toString() != "NaN") {
                            postfixStr[postfixPtr] = postfixStr[postfixPtr] + infixStr[i];
                            i++;
                        }
                        i--;
                        postfixPtr++;
                    }
                    else {
                        while ((!isEmpty(stackArr)) && (prcd(topStack(stackArr), infixStr[i]))) {
                            postfixStr[postfixPtr] = topStack(stackArr);
                            pop_stack(stackArr);
                            postfixPtr++;
                        }
                        if ((!isEmpty(stackArr)) && (infixStr[i] == ")")) {
                            pop_stack(stackArr);
                        }
                        else {
                            push_stack(stackArr, infixStr[i]);
                        }
                    }
                }
                while (!isEmpty(stackArr)) {
                    postfixStr[postfixStr.length] = topStack(stackArr);
                    pop_stack(stackArr);
                }
                var returnVal = new Array();
                for (var i = 0; i < postfixStr.length; i++) {
                    returnVal.push(postfixStr[i]);
                }
                return (returnVal);
            }
            function calculateRPn(postfix) {
                var stos = new Array();
                var arg1, arg2, result;
                var znak;
                for (var i = 0; i < postfix.length; i++) {
                    znak = parseInt(postfix[i]).toString();
                    if (znak != "NaN")
                        stos.push(postfix[i]);
                    else {
                        arg1 = parseInt(stos.pop());
                        arg2 = parseInt(stos.pop());
                        switch (postfix[i]) {
                            case "+":
                                result = arg2 + arg1;
                                break;
                            case "-":
                                result = arg2 - arg1;
                                break;
                            case "*":
                                result = arg2 * arg1;
                                break;
                            case "/":
                                result = arg2 / arg1;
                                break;
                            default:
                                if (stos.length == 0)
                                    return stos[0];
                                else
                                    return "błąd w zapisie";

                        }
                        if (result.toString() == "NaN")
                            return "Brakuje cyfry";
                        stos.push(result);
                    }
                }
                return result;
            }
            
        }
    });

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    
    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }
            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        //args.setPromise();
    };

    app.start();
})();

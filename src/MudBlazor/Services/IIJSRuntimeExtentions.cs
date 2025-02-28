﻿// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MudBlazor
{
    public static class IIJSRuntimeExtentions
    {
        /// <summary>
        /// Invokes the specified JavaScript function asynchronously and catches JSException, JSDisconnectedException and TaskCanceledException
        /// </summary>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
        /// <param name="identifier">An identifier for the function to invoke. For example, the value <c>"someScope.someFunction"</c> will invoke the function <c>window.someScope.someFunction</c>.</param>
        /// <param name="args">JSON-serializable arguments.</param>
        public static async ValueTask InvokeVoidAsyncWithErrorHandling(this IJSRuntime jsRuntime, string identifier, params object[] args)
        {
            try
            {
                await jsRuntime.InvokeVoidAsync(identifier, args);
            }
#if DEBUG
#else
            catch (JSException)
            {
            }
#endif
            catch (JSDisconnectedException)
            {
            }
            catch (TaskCanceledException)
            {
            }
        }

        /// <summary>
        /// Invokes the specified JavaScript function asynchronously and catches JSException, JSDisconnectedException and TaskCanceledException. In case an exception occured the default value of <typeparamref name="TValue"/> is returned
        /// </summary>
        /// <typeparam name="TValue">The JSON-serializable return type.</typeparam>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
        /// <param name="identifier">An identifier for the function to invoke. For example, the value <c>"someScope.someFunction"</c> will invoke the function <c>window.someScope.someFunction</c>.</param>
        /// <param name="args">JSON-serializable arguments.</param>
        /// <returns>An instance of <typeparamref name="TValue"/> obtained by JSON-deserializing the return value into a tuple. The first item (sucess) is true in case where there was no exception, otherwise fall.</returns>
        public static async ValueTask<TValue> InvokeAsyncWithErrorHandling<TValue>(this IJSRuntime jsRuntime, string identifier, params object[] args)
            => await jsRuntime.InvokeAsyncWithErrorHandling(default(TValue), identifier, args);

        /// <summary>
        /// Invokes the specified JavaScript function asynchronously and catches JSException, JSDisconnectedException and TaskCanceledException
        /// </summary>
        /// <typeparam name="TValue">The JSON-serializable return type.</typeparam>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
        /// <param name="fallbackValue">The value that should be returned in case an exception occured</param>
        /// <param name="identifier">An identifier for the function to invoke. For example, the value <c>"someScope.someFunction"</c> will invoke the function <c>window.someScope.someFunction</c>.</param>
        /// <param name="args">JSON-serializable arguments.</param>
        /// <returns>An instance of <typeparamref name="TValue"/> obtained by JSON-deserializing the return value into a tuple. The first item (sucess) is true in case where there was no exception, otherwise fall.</returns>
        public static async ValueTask<TValue> InvokeAsyncWithErrorHandling<TValue>(this IJSRuntime jsRuntime, TValue fallbackValue, string identifier, params object[] args)
        {
            try
            {
                return await jsRuntime.InvokeAsync<TValue>(identifier: identifier, args: args);
            }
#if DEBUG
#else
            catch (JSException)
            {
                return fallbackValue;
            }
#endif
            catch (JSDisconnectedException)
            {
                return fallbackValue;
            }
            catch (TaskCanceledException)
            {
                return fallbackValue;
            }
        }

        /// <summary>
        /// Invokes the specified JavaScript function asynchronously and catches JSException, JSDisconnectedException and TaskCanceledException
        /// </summary>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
        /// <param name="identifier">An identifier for the function to invoke. For example, the value <c>"someScope.someFunction"</c> will invoke the function <c>window.someScope.someFunction</c>.</param>
        /// <param name="args">JSON-serializable arguments.</param>
        /// <returns>A <see cref="ValueTask"/> that represents the asynchronous invocation operation and resolves to true in case no exception has occured ohterwise false.</returns>
        public static async ValueTask<bool> InvokeVoidAsyncWithErrorHandlingAndStatus(this IJSRuntime jsRuntime, string identifier, params object[] args)
        {
            try
            {
                await jsRuntime.InvokeVoidAsync(identifier, args);
                return true;
            }
#if DEBUG
#else
            catch (JSException)
            {
                return false;
            }
#endif
            catch (JSDisconnectedException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }

        /// <summary>
        /// Invokes the specified JavaScript function asynchronously and catches JSException, JSDisconnectedException and TaskCanceledException. In case an exception occured the default value of <typeparamref name="TValue"/> is returned
        /// </summary>
        /// <typeparam name="TValue">The JSON-serializable return type.</typeparam>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
        /// <param name="identifier">An identifier for the function to invoke. For example, the value <c>"someScope.someFunction"</c> will invoke the function <c>window.someScope.someFunction</c>.</param>
        /// <param name="args">JSON-serializable arguments.</param>
        /// <returns>An instance of <typeparamref name="TValue"/> obtained by JSON-deserializing the return value into a tuple. The first item (sucess) is true in case where there was no exception, otherwise fall.</returns>
        public static async ValueTask<(bool success, TValue value)> InvokeAsyncWithErrorHandlingAndStatus<TValue>(this IJSRuntime jsRuntime, string identifier, params object[] args)
            => await jsRuntime.InvokeAsyncWithErrorHandlingAndStatus(default(TValue), identifier, args);

        /// <summary>
        /// Invokes the specified JavaScript function asynchronously and catches JSException, JSDisconnectedException and TaskCanceledException
        /// </summary>
        /// <typeparam name="TValue">The JSON-serializable return type.</typeparam>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
        /// <param name="fallbackValue">The value that should be returned in case an exception occured</param>
        /// <param name="identifier">An identifier for the function to invoke. For example, the value <c>"someScope.someFunction"</c> will invoke the function <c>window.someScope.someFunction</c>.</param>
        /// <param name="args">JSON-serializable arguments.</param>
        /// <returns>An instance of <typeparamref name="TValue"/> obtained by JSON-deserializing the return value into a tuple. The first item (sucess) is true in case where there was no exception, otherwise fall.</returns>
        public static async ValueTask<(bool success, TValue value)> InvokeAsyncWithErrorHandlingAndStatus<TValue>(this IJSRuntime jsRuntime, TValue fallbackValue, string identifier, params object[] args)
        {
            try
            {
                var result = await jsRuntime.InvokeAsync<TValue>(identifier: identifier, args: args);
                return (true, result);
            }
#if DEBUG
#else
            catch (JSException)
            {
                return (false, fallbackValue);
            }
#endif
            catch (JSDisconnectedException)
            {
                return (false, fallbackValue);
            }
            catch (TaskCanceledException)
            {
                return (false, fallbackValue);
            }
        }
    }
}

﻿using MemoryPack;
using System.Text.Json;

namespace SampleApp.Shared;

public abstract class Client
{
    static readonly string BaseAddress = "https://localhost:7287/maui/";
    readonly HttpClient httpClient;
    protected abstract string ContentType { get; }

    protected Client()
    {
        httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
    }

    protected abstract Task<HttpContent> SerializeAsync<T>(T obj, CancellationToken cancellationToken = default);
    protected abstract Task<T?> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default);

    // Message

    public async Task<MessageForCSharp[]> GetMessagesAsync(
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "messages");
        request.Headers.Add("Accept", ContentType);
        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not get messages. Status code: {response.StatusCode}");
        }

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var messages = await DeserializeAsync<MessageForCSharp[]?>(stream, cancellationToken);

        return messages ?? Array.Empty<MessageForCSharp>();
    }

    public async Task<MessageForCSharp?> GetMessageAsync(
        MessageId messageId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"messages/{messageId.AsPrimitive()}");
        request.Headers.Add("Accept", ContentType);
        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not get message. Status code: {response.StatusCode}");
        }

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var message = await DeserializeAsync<MessageForCSharp?>(stream, cancellationToken);

        return message ?? null;
    }

    public async Task PostMessageAsync(
        MessageForCSharp message,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "messages");
        request.Content = await SerializeAsync(message, cancellationToken);
        request.Headers.Add("ContentType", ContentType);

        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not add message. Status code: {response.StatusCode}");
        }
    }

    public async Task PutMessageAsync(
        MessageForCSharp message,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, "messages");
        request.Content = await SerializeAsync(message, cancellationToken);
        request.Headers.Add("ContentType", ContentType);

        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not update message. Status code: {response.StatusCode}");
        }
    }

    public async Task DeleteMessageAsync(
        MessageId messageId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"messages/{messageId.AsPrimitive()}");
        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not delete message. Status code: {response.StatusCode}");
        }
    }

    // Person

    public async Task<PersonForCSharp[]> GetPersonsAsync(
    CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "persons");
        request.Headers.Add("Accept", ContentType);
        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not get persons. Status code: {response.StatusCode}");
        }

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var persons = await DeserializeAsync<PersonForCSharp[]?>(stream, cancellationToken);

        return persons ?? Array.Empty<PersonForCSharp>();
    }

    public async Task<PersonForCSharp?> GetPersonAsync(
        Guid personId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"persons/{personId}");
        request.Headers.Add("Accept", ContentType);
        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not get person. Status code: {response.StatusCode}");
        }

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var person = await DeserializeAsync<PersonForCSharp?>(stream, cancellationToken);

        return person ?? null;
    }

    public async Task PostPersonAsync(
        PersonForCSharp person,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "persons");
        request.Content = await SerializeAsync(person, cancellationToken);
        request.Headers.Add("ContentType", ContentType);

        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not add person. Status code: {response.StatusCode}");
        }
    }

    public async Task PutPersonAsync(
        PersonForCSharp person,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, "persons");
        request.Content = await SerializeAsync(person, cancellationToken);
        request.Headers.Add("ContentType", ContentType);

        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not update person. Status code: {response.StatusCode}");
        }
    }

    public async Task DeletePersonAsync(
        Guid personId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"persons/{personId}");
        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Can not delete person. Status code: {response.StatusCode}");
        }
    }
}

public class JsonClient : Client
{
    protected override string ContentType => "application/json";

    protected override async Task<HttpContent> SerializeAsync<T>(T obj, CancellationToken cancellationToken = default)
    {
        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, obj, cancellationToken: cancellationToken);

        return new StreamContent(stream);
    }

    protected override async Task<T?> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) where T : default
    {
        return await JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }, cancellationToken);
    }
}

public class MemoryPackClient : Client
{
    protected override string ContentType => "application/x-memorypack";

    protected override async Task<HttpContent> SerializeAsync<T>(T obj, CancellationToken cancellationToken = default)
    {
        await using var stream = new MemoryStream();
        await MemoryPackSerializer.SerializeAsync(stream, obj, cancellationToken: cancellationToken);
        return new StreamContent(stream);
    }

    protected override async Task<T?> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) where T : default
    {
        return await MemoryPackSerializer.DeserializeAsync<T>(stream, cancellationToken: cancellationToken);
    }
}
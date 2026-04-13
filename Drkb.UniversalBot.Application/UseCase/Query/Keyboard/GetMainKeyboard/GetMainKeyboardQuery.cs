using MediatR;

namespace Drkb.UniversalBot.Application.UseCase.Query.Keyboard.GetMainKeyboard;

public record GetMainKeyboardQuery(): IRequest<string>;
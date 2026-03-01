using DemoWAS.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SherdProject.DTO;
using System.Net.Http.Json;

namespace DemoWAS.Pages.MainPages
{
    public partial class StartPage 
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private IJSRuntime js { get; set; } = default!;
        [Inject] private UserInfoService UserInfoService { get; set; } = default!;
        [Inject] private IUserService userService { get; set; } = default!;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
                if (firstRender)
                {
                    HttpResponseMessage refreshResponse = new HttpResponseMessage();
                    var UserOnlyResponse = await userService.UserOnly();
                    if (!UserOnlyResponse.IsSuccessStatusCode)
                    {
                        refreshResponse = await userService.RefreshTokin();
                    }
                    if (refreshResponse.IsSuccessStatusCode || UserOnlyResponse.IsSuccessStatusCode)
                    {
                        var userInfoResponse = await UserService.GetUserInfo();
                        if (userInfoResponse.IsSuccessStatusCode)
                        {
                            var user = await userInfoResponse.Content.ReadFromJsonAsync<UserOut>();
                            if (user != null)
                            {
                                UserInfoService.UserOut = user;
                                StateHasChanged();
                            }
                            else
                            {
                                UserInfoService.UserOut = null;
                                UserInfoService.UserOut.Id = 0;
                                StateHasChanged();
                            }
                        }
                        else
                        {
                            UserInfoService.UserOut = null;
                            UserInfoService.UserOut.Id = 0;
                            StateHasChanged();
                        }
                    }
                    NavigationManager.NavigateTo("/home");
                }
            }
        }
    }

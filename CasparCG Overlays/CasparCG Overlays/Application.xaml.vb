Class Application

    ' Ereignisse auf Anwendungsebene wie Startup, Exit und DispatcherUnhandledException
    ' können in dieser Datei verarbeitet werden.

    Protected Overrides Sub OnStartup(e As System.Windows.StartupEventArgs)
        'Dim a = True
        'Do While a
        'Loop
#If False Then
        ApplicationSettings.Generator.CreateSettingsAssembly _
        (
            Helpers.GetAbsolutePath(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location), _
                                                           "..", "..", "..", "SettingsManager.dll")), _
            True, True, _
            <CasparCG_Overlays>
                <MainWindow>
                    <SavedSize>
                        <Width TypeCode="Double" DefaultValue="400.0"/>
                        <Height TypeCode="Double" DefaultValue="500.0"/>
                    </SavedSize>
                </MainWindow>
                <Plugins>
                    <ActiveStates TypeCode="String"/>
                </Plugins>
                <CasparCG>
                    <ServerList TypeCode="String"/>
                </CasparCG>
                <Updates>
                    <EnableAutoUpdates TypeCode="Boolean" DefaultValue="False"/>
                </Updates>
                <IO>
                    <SpecialDirectory Name="AppData" Path="LocalApplicationData">
                        <Directory Name="AppDataDirectory" Path="CasparCG Overlays">
                            <File Name="SettingsXmlFile" Path="Settings.xml"/>
                            <File Name="PluginSettingsXmlFile" Path="PluginSettings.xml"/>
                        </Directory>
                    </SpecialDirectory>
                    <Directory Name="PluginDirectory" Parent="ApplicationDirectory" Path="Plugins"/>
                </IO>
            </CasparCG_Overlays>
        )
        Environment.Exit(0)
        'Debugger.Break()
#End If
        Settings.IO.Initialize(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location))
        Settings.Load(Settings.IO.SettingsXmlFile.Path)
        UpdateInfo.Instance.Initialize()
        If Settings.Updates.EnableAutoUpdates Then
            UpdateInfo.Instance.CheckForUpdates(False)
        End If
        PluginInterfaces.PublicProviders.Initialize(PluginManagement.Settings.PluginSettingsProvider.Instance, _
                                                    CasparServer.Instance, _
                                                    PluginManagement.Compositor.Instance)
        PluginInterfaces.PublicProviders.MefCompositor.AddPluginDirectoryPath(Settings.IO.PluginDirectory.Path, True)
        If Not String.IsNullOrEmpty(Settings.CasparCG.ServerList) Then
            ServerList.CasparCGServerCollection.Instance.FromXml(XElement.Parse(Settings.CasparCG.ServerList))
        End If
        If Not String.IsNullOrEmpty(Settings.Plugins.ActiveStates) Then
            PluginManagement.PluginActiveStates.Instance.FromXml(XElement.Parse(Settings.Plugins.ActiveStates))
        End If
        MyBase.OnStartup(e)
    End Sub

    Protected Overrides Sub OnExit(e As System.Windows.ExitEventArgs)
        PluginManagement.PluginContainer.Instance.UnloadAllPlugins()
        Settings.CasparCG.ServerList = ServerList.CasparCGServerCollection.Instance.ToXml.ToString
        Settings.Plugins.ActiveStates = PluginManagement.PluginActiveStates.Instance.ToXml.ToString
        Settings.Save(Settings.IO.SettingsXmlFile.Path)
        MyBase.OnExit(e)
    End Sub

End Class

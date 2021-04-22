'Codedom Nedir?
'Arkadaşlar Codedom her dilde vardır en basit visual basic6'dan en büyük java dillerine kadar vardır.
'Codedom programımızı hangi programlama dilinde yaptıysak bize o dilde program yazmayı sağlar.
'VB.Net için yaptığımızı düşünelim neredeyse 3Gb Visual Studio yerine kendi yaptığımız bir program ile başka bir bilgisayarda
'VB.Net dilinde program yazabiliriz.
Public Class Form1

    Dim objCodeCompiler As System.CodeDom.Compiler.ICodeCompiler = New VBCodeProvider().CreateCompiler
    Dim objCompilerParameters As New System.CodeDom.Compiler.CompilerParameters()

#Region "Çalıştır"




    Sub Execute()

        objCompilerParameters.ReferencedAssemblies.Add("System.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Core.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Data.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Deployment.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Drawing.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Management.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Xml.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Xml.Linq.dll")
        objCompilerParameters.ReferencedAssemblies.Add("System.Net.dll")

        objCompilerParameters.GenerateInMemory = True

        Dim strCode As String = RichTextBox1.Text

        Dim objCompileResults As System.CodeDom.Compiler.CompilerResults = objCodeCompiler.CompileAssemblyFromSource(objCompilerParameters, strCode)

        If objCompileResults.Errors.HasErrors Then
            RichTextBox2.Text &= vbNewLine & "Hata: Satır>" & objCompileResults.Errors(0).Line.ToString & ", " & objCompileResults.Errors(0).ErrorText
            Exit Sub
        End If

        Dim objAssembly As System.Reflection.Assembly = objCompileResults.CompiledAssembly

        Dim objTheClass As Object = objAssembly.CreateInstance("VBClass")
        If objTheClass Is Nothing Then
            If RichTextBox1.Text = "" Then
                RichTextBox2.Text &= vbNewLine & "Boş Sayfa"
            End If
            Exit Sub
        End If

        Try
            RichTextBox2.Text &= "Kodlar Çözümleniyor..." & vbNewLine
            Execute2()
            RichTextBox2.Text &= vbNewLine & "Dosya Kayıt Ediliyor..." & vbNewLine
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.Desktop & "\")
            RichTextBox1.SaveFile(My.Computer.FileSystem.SpecialDirectories.Desktop & "\test.vb", RichTextBoxStreamType.RichText)


            RichTextBox2.Text &= "Kodlar Tekrar Çözümleniyor..." & vbNewLine
            RichTextBox1.LoadFile(My.Computer.FileSystem.SpecialDirectories.Desktop & "\test.vb")
            Execute2()
            RichTextBox2.Text &= vbNewLine & "Başlatıldı.."
            objTheClass.GetType.InvokeMember("Code", System.Reflection.BindingFlags.InvokeMethod, Nothing, objTheClass, Nothing)
        Catch ex As Exception
            RichTextBox2.Text &= vbNewLine & "Hata:" & ex.Message

        End Try
    End Sub
#End Region

#Region "Hata Tara"
    Sub Execute2()


        Dim strCode As String = RichTextBox1.Text


        Dim objCompileResults As System.CodeDom.Compiler.CompilerResults = objCodeCompiler.CompileAssemblyFromSource(objCompilerParameters, strCode)


        If objCompileResults.Errors.HasErrors Then
            RichTextBox2.Text &= vbNewLine & "Hata: Satır>" & objCompileResults.Errors(0).Line.ToString & ", " & objCompileResults.Errors(0).ErrorText
            Exit Sub
        End If

        Dim objAssembly As System.Reflection.Assembly = objCompileResults.CompiledAssembly

        Dim objTheClass As Object = objAssembly.CreateInstance("VBClass")
        If objTheClass Is Nothing Then
            If RichTextBox1.Text = "" Then
                RichTextBox2.Text &= vbNewLine & "Boş Sayfa"
            End If
            Exit Sub
        End If

    End Sub
#End Region

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If Panel1.Visible = True Then
            Panel1.Visible = False
        Else
            Panel1.Visible = True
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        If Panel1.Visible = False Then
            Panel1.Visible = True
        End If

        RichTextBox2.Clear()
        Execute()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class

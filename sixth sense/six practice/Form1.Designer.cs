Imports System
Imports System.Collections
Imports System.Reflection

Namespace WUW01
    Public Class Category
        Private _name As String

        Private _prototypes As ArrayList

        Default Public ReadOnly Property Item(ByVal i As Integer) As Gesture
            Get
                Dim gesture As WUW01.Gesture
                If(If(0 > i, True, i >= Me._prototypes.Count)) Then
                   gesture = Nothing
                Else
                    gesture = DirectCast(Me._prototypes(i), WUW01.Gesture)
                End If
                Return gesture
            End Get
        End Property

        Public ReadOnly Property Name As String
            Get
                Return Me._name
            End Get
        End Property

        Public ReadOnly Property NumExamples As Integer
            Get
                Return Me._prototypes.Count
            End Get
        End Property

        Public Sub New(ByVal name As String)
            MyBase.New()
            Me._name = name
            Me._prototypes = Nothing
        End Sub

        Public Sub New(ByVal name As String, ByVal firstExample As Gesture)
            MyBase.New()
            Me._name = name
            Me._prototypes = New ArrayList()
            Me.AddExample(firstExample)
        End Sub

        Public Sub New(ByVal name As String, ByVal examples As ArrayList)
            MyBase.New()
            Me._name = name
            Me._prototypes = New ArrayList(examples.Count)
            Dim i As Integer = 0
            While i < examples.Count
                Me.AddExample(DirectCast(examples(i), Gesture))
                i = i + 1
            End While
        End Sub

        Public Sub AddExample(ByVal p As Gesture)
            Dim success As Boolean = True
            Try
                If(Category.ParseName(p.Name) <> Me._name) Then
                   Throw New System.ArgumentException("Prototype name does not equal the name of the category to which it was added.")
                End If
                Dim i As Integer = 0
                While i<Me._prototypes.Count
                    If (DirectCast(Me._prototypes(i), Gesture).Name = p.Name) Then
                        Throw New System.ArgumentException("Prototype name was added more than once to its category.")
                    End If
                    i = i + 1
                End While
            Catch argumentException As System.ArgumentException
                Console.WriteLine(argumentException.Message)
                success = False
            End Try
            If (success) Then
                Me._prototypes.Add(p)
            End If
        End Sub

        Public Shared Function ParseName(ByVal s As String) As String
            Dim category As String = String.Empty
            Dim i As Integer = s.Length - 1
            While i >= 0
                If(Char.IsDigit(s(i))) Then
                   i = i - 1
                Else
                    category = s.Substring(0, i + 1)
                    Exit While
                End If
            End While
            Return category
        End Function
    End Class
End Namespace
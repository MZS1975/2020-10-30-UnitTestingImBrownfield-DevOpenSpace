using System;
using Machine.Specifications;

namespace ConferenceDude.Domain.Specs
{
    [Subject(typeof(SessionValidator))]
    internal class Wenn_eine_valide_Session_validiert_wird
    {
        Establish context = () =>
        {
            _sut = new SessionValidator();
            _session = new Session() {Id = 1, Title = "Title", Abstract = "Abstract"};
        };

        Because of = () => _result = _sut.IsValid(_session, out _message);

        It soll_der_Validator_valide_liefern = () => { _result.ShouldBeTrue(); };
        It soll_die_Fehlermeldung_leer_sein = () => { _message.ShouldBeEmpty(); };

        static SessionValidator _sut;
        static Session _session;
        static bool _result;
        static string _message;
    }

    [Subject(typeof(SessionValidator))]
    internal class Wenn_eine_Session_mit_leerem_Titel_validiert_wird
    {
        Establish context = () =>
        {
            _sut = new SessionValidator();
            _session = new Session() {Id = 1, Abstract = "Abstract"};
        };

        Because of = () => _result = _sut.IsValid(_session, out _message);

        It soll_der_Validator_nicht_valide_liefern = () => { _result.ShouldBeFalse(); };
        It soll_die_Fehlermeldung_Titel_enthalten = () => { _message.ShouldContain("Titel"); };
        It soll_die_Fehlermeldung_muss_enthalten = () => { _message.ShouldContain("muss"); };

        static SessionValidator _sut;
        static Session _session;
        static bool _result;
        static string _message;
    }

    [Subject(typeof(SessionValidator))]
    internal class Wenn_eine_Session_mit_leerem_Abstrakt_validiert_wird
    {
        Establish context = () =>
        {
            _sut = new SessionValidator();
            _session = new Session() {Id = 1, Title = "Title"};
        };

        Because of = () => _result = _sut.IsValid(_session, out _message);

        It soll_der_Validator_nicht_valide_liefern = () => { _result.ShouldBeFalse(); };
        It soll_die_Fehlermeldung_Abstrakt_enthalten = () => { _message.ShouldContain("Abstrakt"); };
        It soll_die_Fehlermeldung_muss_enthalten = () => { _message.ShouldContain("muss"); };

        static SessionValidator _sut;
        static Session _session;
        static bool _result;
        static string _message;
    }

    [Subject(typeof(Session))]
    internal class Wenn_eine_Session_eine_Id_hat
    {
        Establish context = () =>
        {
            _sut = new Session() {Id = 1, Title = "Title", Abstract = "Abstract" };
        };

        Because of = () => _result = _sut.IsNew;

        It soll_sie_nicht_neu_sein = () => { _result.ShouldBeFalse(); };

        static Session _sut;
        static bool _result;
        static string _message;
    }

    [Subject(typeof(Session))]
    internal class Wenn_eine_Session_keine_Id_hat
    {
        Establish context = () =>
        {
            _sut = new Session() {Title = "Title", Abstract = "Abstract" };
        };

        Because of = () => _result = _sut.IsNew;

        It soll_sie_neu_sein = () => { _result.ShouldBeTrue(); };

        static Session _sut;
        static bool _result;
        static string _message;
    }
}
